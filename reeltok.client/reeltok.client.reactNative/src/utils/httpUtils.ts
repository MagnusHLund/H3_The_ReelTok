import { HttpMethod, PayloadType } from '../services/httpService'

export function prepareHttpRequestBody<T>(body: T) {
  if (body === undefined) {
    return undefined
  }

  return JSON.stringify(body)
}

export function prepareHttpRequestWithQueryParameters<TBody>(url: URL, body: TBody) {
  const queryParameters = new URLSearchParams()
  Object.entries(body as Record<string, string>).forEach(([key, value]) => {
    queryParameters.append(key, value)
  })

  url.search = queryParameters.toString()
  return url
}

export function determinePayloadType<TBody>(method: HttpMethod, body: TBody): PayloadType {
  if (body === undefined) {
    return undefined
  }

  if (method === 'GET' || method === 'DELETE') {
    return 'queryParameters'
  }

  return 'requestBody'
}
