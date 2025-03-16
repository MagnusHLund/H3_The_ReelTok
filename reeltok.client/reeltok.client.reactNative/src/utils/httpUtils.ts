import { HttpMethod } from './../services/httpService'
import { PayloadType } from '../services/httpService'
import { AxiosRequestConfig } from 'axios'

const baseAxiosRequestConfig: AxiosRequestConfig = {
  headers: {
    accept: 'application/json',
  },
}

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
    ...baseAxiosRequestConfig,
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
    ...baseAxiosRequestConfig,
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
      // Map URLs as files
      const file = {
        uri: value.toString(),
        name: key,
        type: 'application/octet-stream', // Default MIME type for files
      }
      formData.append(key, file as any) // Add the file to the form-data
    } else if (typeof value === 'string' || typeof value === 'number' || value === null) {
      // Convert other values to strings and add them as text
      formData.append(key, value?.toString() || '')
    } else {
      console.warn(`Unexpected data type for key "${key}" - skipping`)
    }
  })

  return {
    url: url.toString(),
    method: httpMethod,
    headers: {
      'Content-Type': 'multipart/form-data',
    },
    data: formData,
    ...baseAxiosRequestConfig,
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
