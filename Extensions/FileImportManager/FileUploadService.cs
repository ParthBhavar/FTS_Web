using FTS.Configuration;
using Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.Extensions.FileProviders;
using System.Drawing;
using System.Drawing.Drawing2D;
using Azure.Storage.Blobs;

namespace FileManager
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly ILogging _logger;             
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileUploadService(IAppConfiguration appConfiguration,
            ILoggerFactory loggerFactory,
            IWebHostEnvironment hostingEnvironment)
        {
            _appConfiguration = appConfiguration;
            _logger = loggerFactory.CreateLogger<FileUploadService>();            
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// Method to upload document
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="NewfileName">Name of the newfile.</param>
        /// <param name="DocumentPath">The document path.</param>
        /// <returns>
        /// return true false for successfully uploaded or not
        /// </returns>
        public bool UploadDocument(IFormFile file, string NewfileName, string DocumentPath)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return false;

                var folderpath = Path.Combine(Directory.GetCurrentDirectory(), DocumentPath);

                bool folderExists = Directory.Exists(folderpath);
                if (!folderExists)
                    Directory.CreateDirectory(folderpath);

                var path = Path.Combine(folderpath, NewfileName);

                using (var stream = new FileStream(path, FileMode.Create,FileAccess.ReadWrite))
                {
                    file.CopyTo(stream);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="NewfileName">The NewFilename.</param>
        /// <returns>
        /// Download the file in Browser
        /// </returns>
        public FileStreamResult DownloadDocument(string filePath, string NewfileName)
        {
            try
            {
                if (filePath == null && NewfileName == null)
                    return null;

                var fileContent = ReadAllBytes(filePath);
                var stream = new MemoryStream(fileContent);
                var fileStreamResult = new FileStreamResult(stream, GetContentType(filePath))
                {
                    FileDownloadName = NewfileName
                };
                return fileStreamResult;

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>retrunt the content type</returns>
        public string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

      
        /// <summary>
        /// Upload Base 64 string to local
        /// </summary>
        /// <param name="base64String"></param>
        /// <param name="documentFolder"></param>
        /// <returns></returns>
        public async Task<string> UploadBase64ToLocal(string base64String, string documentFolder,string fileName)
        {
            Stream memory = GetStreamFromBase64(base64String);
            return await UploadFileInLocalUsingStream(memory, documentFolder, fileName);
        }

        /// <summary>
        /// Upload string to local
        /// </summary>
        /// <param name="str"></param>
        /// <param name="documentFolder"></param>
        /// <param name="htmlFileName"></param>
        /// <returns></returns>
        public async Task<string> UploadStringToLocal(string str, string documentFolder, string htmlFileName)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(str);
                    writer.Flush();
                    stream.Position = 0;
                    return await UploadFileInLocalUsingStream(stream, documentFolder, htmlFileName);
                }
            }
        }

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        /// <returns>MIME types</returns>
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.ms-word" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" },
                { ".csv", "text/csv" }
            };
        }

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Byte array of file</returns>
        private byte[] ReadAllBytes(string fileName)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }

        /// <summary>
        /// Get Api full path based on relative path
        /// </summary>
        /// <param name="relativePath"></param>
        /// <returns></returns>
        public string GetApiAbsolutePath(string relativePath)
        {
            try
            {
                var uri = new Uri(_appConfiguration.WebSettings.ApiUrl);

                return $"{uri.ToString()}{relativePath}";
            }
            catch(Exception ex)
            {
                _logger.Error(GetType().Name + " => " + MethodBase.GetCurrentMethod().Name, ex);
                return string.Empty;
            }
        }

       
        /// <summary>
        /// get Base64 for Image
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public string GetBase64ForImage(string imagePath)
        {
            using (FileStream fileStream = File.OpenRead(imagePath))
            {
                MemoryStream memStream = new MemoryStream();
                memStream.SetLength(fileStream.Length);
                fileStream.Read(memStream.GetBuffer(), 0, (int)fileStream.Length);
                fileStream.CopyTo(memStream);
                memStream.Position = 0;
                byte[] bytes = memStream.ToArray();
                return FileUploadConstant.BASE64CONSTANT + Convert.ToBase64String(bytes);
            }
        }        
       

        /// <summary>
        /// get base64 from stream
        /// </summary>
        /// <param name="memStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetBase64FromStream(MemoryStream memStream, string fileName)
        {
            string contentType = GetContentType(fileName);
            fileName = Convert.ToBase64String(memStream.ToArray());
            return "data:" + contentType + ";base64," + fileName;

        }

        /// <summary>
        /// get stream from base64
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public Stream GetStreamFromBase64(string base64String)
        {
            base64String = base64String.IndexOf(',') > -1 ? base64String.Split(",")[1] : base64String;
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Stream memory = new MemoryStream(imageBytes);
            return memory;

        }

       

        /// <summary>
        /// upload file in blob using memory stream
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="documentFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<string> UploadFileInLocalUsingStream(Stream memoryStream, string documentFolder, string fileName = "")
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string folderPath = Path.Combine(webRootPath, documentFolder);

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string newFileName = string.IsNullOrEmpty(fileName) ? $"{Guid.NewGuid().ToString()}.png" : fileName;
                string fullPath = Path.Combine(folderPath, newFileName);
                var bytes = GetBytesFromStream(memoryStream);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await stream.WriteAsync(bytes, 0, bytes.Length);
                    stream.Flush();
                }

                return documentFolder + newFileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Download stream from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Stream DownloadStreamFromUrl(string url)
        {

            try
            {
                // Fixed : Getting "A connection attempt failed because the connected party did not properly respond after a period of time" error on server
                // https://stackoverflow.com/questions/17693353/a-connection-attempt-failed-because-the-connected-party-did-not-properly-respon/19516129#19516129
                Uri fileUri = new Uri(url, UriKind.Absolute);
                using (WebClient client = new WebClient())
                {
                    return new MemoryStream(client.DownloadData(fileUri));
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Exception from DownloadStreamFromUrl \n Exception : {ex.ToString()}");
                return null;
            }
        }

        /// <summary>
        /// Download file stream from wwwroot folder
        /// </summary>
        /// <param name="filePath">File path</param>        
        /// <returns></returns>
        public Stream DownloadFileFromWwwRoot(string filePath)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;            
            IFileProvider provider = new PhysicalFileProvider(webRootPath);
            IFileInfo fileInfo = provider.GetFileInfo(filePath);
            var stream = fileInfo.CreateReadStream();
            return stream;
        }


        public string ResizeImage(string imagePath,int width,int height)
        {         
            Image image;

            //Convert byte[] into image
            using (FileStream fileStream = File.OpenRead(imagePath))
            {
                image = Image.FromStream(fileStream);
            }

            // Resize the image
            Bitmap b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, 0, 0, width, height);
            g.Dispose();
            image = (Image)b;

            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                return FileUploadConstant.BASE64CONSTANT + Convert.ToBase64String(imageBytes);

            }
        }

        #region Private Methods

        private byte[] GetBytesFromStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        #endregion
    }
    public static class FileUploadConstant
    {
        public const string BASE64CONSTANT = "data:image/png;base64,";
    }

    
}
