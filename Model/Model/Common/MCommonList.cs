using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Common
{
    public class MCommonList
    {
        public int DataValue { get; set; }
        public string DisplayValue { get; set; }
        public int ParentId { get; set; }
       


    }

    public class MEmployeeData
    {
        public string Email { get; set; }
        public string MobileNo { get; set; }
    }

    public class MTradeUnion
    {
        public int DataValue { get; set; }
        public string DisplayValue { get; set; }
        public string RegistrationNo { get; set; }
        public string PAddress { get; set; }
        public string SAddress { get; set; }
        public int TalukaID { get; set; }
        public int DistrictID { get; set; }
        public int Pincode { get; set; }
    }

    public class MEstablishment
    {
        public int DataValue { get; set; }
        public string DisplayValue { get; set; }
        public string EstablishmentCode { get; set; }
        public string PAddress { get; set; }
        public string SAddress { get; set; }
        public int TalukaID { get; set; }
        public int DistrictID { get; set; }
        public int Pincode { get; set; }
    }

    public class MArea
    {
        public int DataValue { get; set; }
        public string DisplayValue { get; set; }
        public int ZoneID { get; set; }
    }

    public class ddlCommanModel
    {
        public int key { get; set; }
        public string text { get; set; }
    }
}


