using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HttpRequestHandler
{
    public interface ISerializer
    {
        Stream Serialize<T>(T t);
    }
}
