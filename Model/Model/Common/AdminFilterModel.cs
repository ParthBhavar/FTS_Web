using System.Collections.Generic;
using System.Linq;

namespace FTS.Model.Common
{
    public class AdminFilterModel : FilterModel
    {
        public string BtwNumber { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public List<int> Status { get; set; }

        public string GetStatus()
        {
            if (Status != null && Status.Any())
            {
                return string.Join(',', Status);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
