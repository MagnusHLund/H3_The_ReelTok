import { HttpMethod } from './../services/httpService'
import { PayloadType } from '../services/httpService'
import { AxiosRequestConfig } from 'axios'

export function prepareJsonRequestBody<TRequestDto>(
  body: TRequestDto,
  url: URL,
  httpMethod: HttpMethod
): AxiosRequestConfig {
  const jsonStringBody = JSON.stringify(body)

  return {
    url: url.toString(),
    method: httpMethod,
    headers: {
      'Content-Type': 'application/json',
    },
    data: jsonStringBody,
  }
}

export function prepareQueryParametersRequest<TRequestDto>(
  body: TRequestDto,
  url: URL,
  httpMethod: HttpMethod
): AxiosRequestConfig {
  const queryParameters = new URLSearchParams()
  Object.entries(body as Record<string, string>).forEach(([key, value]) => {
    queryParameters.append(key, value)
  })

  url.search = queryParameters.toString()

  return {
    url: url.toString(),
    method: httpMethod,
  }
}

export function prepareMultipartFormDataRequestBody<TRequestDto>(
  body: TRequestDto,
  url: URL,
  httpMethod: HttpMethod
): AxiosRequestConfig {
  const formData = new FormData()

  Object.entries(body as Record<string, unknown>).forEach(([key, value]) => {
    if (value instanceof URL) {
      formData.append(key, {
        uri: value,
        name: key,
        type: 'application/octet-stream', // Default MIME type for files
      } as any)
    } else {
      formData.append(key, value?.toString() || '')
    }
  })

  return {
    url: url.toString(),
    method: httpMethod,
    headers: {
      'Content-Type': 'multipart/form-data',
    },
    data: formData,
  }
}

export function prepareHttpPayload<TRequestDto>(
  body: TRequestDto,
  url: URL,
  httpMethod: HttpMethod,
  payloadType: PayloadType
): AxiosRequestConfig {
  if (payloadType === 'JsonBody') {
    return prepareJsonRequestBody(body, url, httpMethod)
  } else if (payloadType === 'queryParameters') {
    return prepareQueryParametersRequest(body, url, httpMethod)
  } else {
    return prepareMultipartFormDataRequestBody(body, url, httpMethod)
  }
}
