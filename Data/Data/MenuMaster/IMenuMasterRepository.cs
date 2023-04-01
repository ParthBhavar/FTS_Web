using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.MenuMaster
{
    public interface IMenuMasterRepository
    {
        List<MenuMasterModel> MenuList();

        MenuMasterModel MenuRecord(int MenuId);

        MenuMasterModel SaveMenuRecord(MenuMasterModel ObjMenu);

        MenuMasterModel DeleteMenuRecord(int UserID, int MenuId);


    }
}
