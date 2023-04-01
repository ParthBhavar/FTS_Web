using FTS.Model.Common;

namespace FTS.Model.OrderModule
{
    public class OrderPaginationResponse<T>: PaginationResponse<T> where T : class
    {
        public decimal TotalAmount { get; set; }
        public decimal TotalAmount2 { get; set; }
    }
}
