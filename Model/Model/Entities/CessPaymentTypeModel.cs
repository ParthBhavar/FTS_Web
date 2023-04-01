using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class CessPaymentTypeModel : BaseEntity
    {
        public int CessPaymentTypeIDEdit { get; set; }
        public string CessPaymentID { get; set; }
        public int CessPaymnetType { get; set; }
    }
}
