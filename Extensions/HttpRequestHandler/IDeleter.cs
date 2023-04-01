using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HttpRequestHandler
{
    public interface IDeleter
    {
        Stream DeleteStream(string url, string token, List<Dictionary<string, string>> headers = null);
    }
}
