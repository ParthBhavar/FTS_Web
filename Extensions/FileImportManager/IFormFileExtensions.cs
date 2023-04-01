using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FileManager
{
    public static class IFormFileExtensions
    {
        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Return File Name</returns>
        public static string GetFilename(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                            file.ContentDisposition).FileName.ToString().Trim('"');
        }

        /// <summary>
        /// Gets the file stream.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Return File Stream</returns>
        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        /// <summary>
        /// Gets the file array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>
        /// ReturnFileArray
        /// </returns>
        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            MemoryStream filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }
    }
}
