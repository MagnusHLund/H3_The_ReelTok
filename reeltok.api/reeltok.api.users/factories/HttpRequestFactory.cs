using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;

namespace reeltok.api.users.Factories
{
    public static class HttpRequestFactory
    {
        public static HttpRequestMessage CreateHttpRequest<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod,
            bool isMultipartFormData
        )
        {
            if (httpMethod == HttpMethod.Get || httpMethod == HttpMethod.Delete)
            {
                return PrepareHttpRequestWithQueryParameters(requestDto, targetUrl, httpMethod);
            }
            else if (isMultipartFormData)
            {
                return PrepareMultipartFormDataRequest(requestDto, targetUrl, httpMethod);
            }
            else
            {
                return PrepareHttpRequestBody(requestDto, targetUrl, httpMethod);
            }
        }

        private static HttpRequestMessage PrepareHttpRequestBody<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod
        )
        {
            string requestContent = JsonConvert.SerializeObject(requestDto);
            return new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = CreateStringContent(requestContent)
            };
        }

        private static HttpRequestMessage PrepareMultipartFormDataRequest<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod
        )
        {
            MultipartFormDataContent formDataContent = new MultipartFormDataContent();
            foreach (PropertyInfo property in typeof(TRequest).GetProperties())
            {
                object? value = property.GetValue(requestDto);
                if (value != null)
                {
                    StringContent stringContent = new StringContent(value.ToString() ?? string.Empty);
                    stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = property.Name
                    };
                    formDataContent.Add(stringContent);
                }
            }
            return new HttpRequestMessage(httpMethod, targetUrl)
            {
                Content = formDataContent
            };
        }

        private static HttpRequestMessage PrepareHttpRequestWithQueryParameters<TRequest>(
            TRequest requestDto,
            Uri targetUrl,
            HttpMethod httpMethod
        )
        {
            Dictionary<string, string> requestQueryParameters = ConvertRequestDtoToQueryParameters(requestDto);
            string targetUrlWithQueryParameters = QueryHelpers.AddQueryString(targetUrl.ToString(), requestQueryParameters);

            return new HttpRequestMessage(httpMethod, targetUrlWithQueryParameters);
        }

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private static Dictionary<string, string> ConvertRequestDtoToQueryParameters<TRequest>(TRequest request)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo properties in typeof(TRequest).GetProperties())
            {
                object? value = properties.GetValue(request);
                if (value != null)
                {
                    dictionary.Add(properties.Name, value.ToString());
                }
            }
            return dictionary;
        }
    }
}