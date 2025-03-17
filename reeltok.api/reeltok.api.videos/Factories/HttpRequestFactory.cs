using System.Text;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections;
using System.Net.Http.Headers;

namespace reeltok.api.videos.Factories
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
                    if (value is IFormFile formFile)
                    {
                        StreamContent fileContent = new StreamContent(formFile.OpenReadStream());
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = property.Name,
                            FileName = formFile.FileName
                        };
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);
                        formDataContent.Add(fileContent);
                    }
                    else
                    {
                        StringContent stringContent = new StringContent(value.ToString() ?? string.Empty);
                        stringContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                        {
                            Name = property.Name
                        };
                        formDataContent.Add(stringContent);
                    }
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
            var requestQueryParameters = ConvertRequestDtoToQueryParameters(requestDto);

            // Manually build the query string to support repeated keys
            var queryString = string.Join("&", requestQueryParameters.Select(kvp =>
                $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));

            string targetUrlWithQueryParameters = $"{targetUrl}?{queryString}";

            return new HttpRequestMessage(httpMethod, targetUrlWithQueryParameters);
        }

        private static StringContent CreateStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private static List<KeyValuePair<string, string>> ConvertRequestDtoToQueryParameters<TRequest>(TRequest request)
        {
            var queryParameters = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo property in typeof(TRequest).GetProperties())
            {
                object? value = property.GetValue(request);

                if (value != null)
                {
                    if (value is IEnumerable enumerable && !(value is string)) // Handle any IEnumerable except string
                    {
                        foreach (var item in enumerable)
                        {
                            queryParameters.Add(new KeyValuePair<string, string>(property.Name, item.ToString() ?? string.Empty));
                        }
                    }
                    else // Handle single values
                    {
                        queryParameters.Add(new KeyValuePair<string, string>(property.Name, value.ToString() ?? string.Empty));
                    }
                }
            }

            return queryParameters;
        }
    }
}