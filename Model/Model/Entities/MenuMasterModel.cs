using FTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Model.Entities
{
    public class MenuMasterModel : BaseEntity
    {
        public int MenuId { get; set; }

        public int MenuIdEdit { get; set; }

        public int ParentMenuId { get; set; }

        public string MenuName { get; set; }

        public string MenuDescription { get; set; }

        public string PageName { get; set; }

        public string PageURL { get; set; }

        public int? NodeLevel { get; set; }
        public string Icon { get; set; }



        public List<MCommonList> MenuList { get; set; }
        public List<MCommonList> AssignMenuList { get; set; }
        public int IsAssign { get; set; }
        public int DataValue { get; set; }
        public string DisplayValue { get; set; }
    }
}
