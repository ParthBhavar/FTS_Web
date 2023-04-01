using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HttpRequestHandler
{
    public class Serializer : ISerializer
    {
        public Stream Serialize<T>(T t)
        {
            string requestedJsonParameters = JsonConvert.SerializeObject(t);
            var bytesRequestParameters = Encoding.UTF8.GetBytes(requestedJsonParameters);
            return new MemoryStream(bytesRequestParameters);
        }
    }
}
