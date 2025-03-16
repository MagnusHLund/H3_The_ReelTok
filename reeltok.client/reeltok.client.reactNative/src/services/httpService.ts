import { prepareHttpPayload } from '../utils/httpUtils'
import axios from 'axios'

axios.defaults.headers.post['Content-Type'] = '' // Reset default Content-Type
axios.defaults.headers.common['Content-Type'] = '' // Just in case for all requests

export type PayloadType = 'JsonBody' | 'FormDataBody' | 'queryParameters'
export type HttpMethod = 'GET' | 'POST' | 'PUT' | 'DELETE'

interface HttpServiceProps<TRequestDto> {
  httpMethod: HttpMethod
  url: string
  body: TRequestDto
  payloadType: PayloadType
}

const baseUrl = 'https://api.reeltok.site/api/'

async function httpService<TRequestDto = undefined>({
  httpMethod,
  url,
  body,
  payloadType = 'JsonBody',
}: HttpServiceProps<TRequestDto>) {
  const requestUrl = new URL(`${baseUrl}${url}`)
  const requestConfig = prepareHttpPayload(body, requestUrl, httpMethod, payloadType)

  try {
    const response = await axios(requestConfig)
    return response
  } catch (error) {
    console.error(error)
  }
}

export default httpService
