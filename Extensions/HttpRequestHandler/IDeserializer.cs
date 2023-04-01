using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestHandler
{
    public interface IDeserializer
    {
        T Deserialize<T>(Stream stream);

        Task<T> DeserializeAsync<T>(Stream stream);
    }
}
