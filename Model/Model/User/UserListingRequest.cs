
using FTS.Model.Common;

namespace FTS.Model.User
{
    public class UserListingRequest : PaginationRequest
    {        

        public string Mobile { get; set; }

        public string UserNo { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }

        public bool? IsActive { get; set; }
        
    }
}
