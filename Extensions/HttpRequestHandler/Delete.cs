using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace HttpRequestHandler
{
    public class Delete : IDeleter
    {
        public Stream DeleteStream(string url, string token, List<Dictionary<string, string>> headers = null)
        {
            return GetResponse(url, token, headers).GetResponseStream();
        }

        private HttpWebResponse GetResponse(string url, string token, List<Dictionary<string, string>> headers)
        {
            var request = CreateAndSetupRequest(url, token, headers);
            var response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception();
            }

            return response;
        }

        private HttpWebRequest CreateAndSetupRequest(string url, string token, List<Dictionary<string, string>> headers)
        {
            var request = CreateHttpWebRequest(url);
            request.Headers["Authorization"] = "Bearer " + token;

            if (headers != null && headers.Count > 0)
            {
                foreach (var dictionary in headers)
                {
                    foreach (var keyValue in dictionary)
                    {
                        request.Headers[keyValue.Key] = keyValue.Value;
                    }
                }
            }

            request.Method = "DELETE";
            request.ContentType = "application/json";
            return request;
        }

        protected virtual HttpWebRequest CreateHttpWebRequest(string url)
        {
            return (HttpWebRequest)WebRequest.Create(url);
        }
    }
}
