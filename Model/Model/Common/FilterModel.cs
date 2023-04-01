namespace FTS.Model.Common
{
    public class FilterModel : PaginationRequest
    {
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; }
        public string SearchExpression { get; set; }        
    }
}
