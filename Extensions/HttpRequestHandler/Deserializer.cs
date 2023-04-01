using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHandler
{
    public class Deserializer : IDeserializer
    {
        public T Deserialize<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return await JsonConvert.DeserializeObject<Task<T>>(reader.ReadToEndAsync().Result);
            }
        }
    }
}
