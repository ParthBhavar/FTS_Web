using System.Runtime.CompilerServices;

namespace FTS.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetClassName([CallerFilePath] string fileName = "") => fileName;

        public static string GetMethodName([CallerMemberName] string methodName = "") => methodName;
    }
}
