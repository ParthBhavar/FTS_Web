using System.ComponentModel.DataAnnotations;

namespace FTS.Model.Common
{
    public class PaginationRequest
    {
        public string Id { get; set; }
        [Required]
        public int PageNo { get; set; }
        [Required]
        public int PageSize { get; set; }
        public string SearchText { get; set; }
        public int SortOrder { get; set; }
        public string SortBy { get; set; }

        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public string SearchColumn { get; set; }

    }
}
