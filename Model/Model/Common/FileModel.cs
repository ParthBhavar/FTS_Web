using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FTS.Model.Common
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string Base64 { get; set; }
        public string FileExtension { get; set; }
        public string BlobUrl { get; set; }
        public List<IFormFile> Files { get; set; }
        public int ApplicationID { get; set; }
        public bool IsResponse { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
