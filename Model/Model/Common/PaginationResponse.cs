using System.Collections.Generic;

namespace FTS.Model.Common
{
    public class PaginationResponse<T> where T : class
    {
        public PaginationResponse()
        {
            Data = new List<T>();
        }
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
    }
}
