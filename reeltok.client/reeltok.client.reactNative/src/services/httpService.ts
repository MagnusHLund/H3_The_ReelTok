import axios, { AxiosRequestConfig } from 'axios'
import {
  determinePayloadType,
  prepareHttpRequestBody,
  prepareHttpRequestWithQueryParameters,
} from '../utils/httpUtils'

export type PayloadType = 'requestBody' | 'queryParameters' | undefined
export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE'

interface HttpServiceProps<TBody> {
  method: HttpMethod
  url: URL
  body: TBody
}

async function httpService<TBody = undefined>({ method, url, body }: HttpServiceProps<TBody>) {
  const payloadType = determinePayloadType<TBody>(method, body)

  const config: AxiosRequestConfig = {
    url: url.toString(),
    method: method,
    headers: {
      'Content-Type': 'application/json',
    },
    data: payloadType === 'requestBody' ? prepareHttpRequestBody<TBody>(body) : undefined,
  }

  if (payloadType === 'queryParameters' && body !== undefined) {
    url = prepareHttpRequestWithQueryParameters(url, body)
    config.url = url.toString()
  }

  try {
    const response = await axios(config)
    return response
  } catch (error) {
    console.error(error)
  }
}

export default httpService
