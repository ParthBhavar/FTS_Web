using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace FileManager
{
    public interface IFileUploadService
    {
        /// <summary>
        /// Method to upload document
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="NewfileName">Name of the newfile.</param>
        /// <param name="DocumentPath">The document path.</param>
        /// <returns>
        /// return true false for successfully uploaded or not
        /// </returns>
        bool UploadDocument(IFormFile file, string NewfileName, string DocumentPath);

        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="NewfileName">The NewFilename.</param>
        /// <returns>
        /// Download the file in Browser
        /// </returns>
        FileStreamResult DownloadDocument(string filePath, string NewfileName);

        /// <summary>
        /// Get content type from path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string GetContentType(string path);

     

        /// <summary>
        /// Get Base64 for Image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        string GetBase64ForImage(string imagePath);
        

        /// <summary>
        /// get base64 from stream
        /// </summary>
        /// <param name="memStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetBase64FromStream(MemoryStream memStream, string fileName);

        /// <summary>
        /// get stream from base64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        Stream GetStreamFromBase64(string base64String);              

        /// <summary>
        /// Upload Base 64 string to local
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="documentFolder"></param>
        /// <returns></returns>
        Task<string> UploadBase64ToLocal(string base64String, string documentFolder,string fileName);

        /// <summary>
        /// Upload string to local
        /// </summary>
        /// <param name="str"></param>
        /// <param name="documentFolder"></param>
        /// <param name="htmlFileName"></param>
        /// <returns></returns>
        Task<string> UploadStringToLocal(string str, string documentFolder, string htmlFileName);

        /// <summary>
        /// upload file in blob using memory stream
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="documentFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Task<string> UploadFileInLocalUsingStream(Stream memoryStream, string documentFolder, string fileName = "");

        /// <summary>
        /// Download stream from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Stream DownloadStreamFromUrl(string url);

        /// <summary>
        /// Get Api full path based on relative path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        string GetApiAbsolutePath(string relativePath);

        /// <summary>
        /// Download file stream from wwwroot folder
        /// </summary>
        /// <param name="filePath">File path</param>        
        /// <returns></returns>
        Stream DownloadFileFromWwwRoot(string filePath);

        /// <summary>
        /// Resize large image into small
        /// </summary>
        /// <param name="imagePath">Image path</param>        
        /// <returns></returns>
        string ResizeImage(string imagePath,int width,int height);
    }
}
