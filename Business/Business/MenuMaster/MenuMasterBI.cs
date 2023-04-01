using FTS.Data.MenuMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.MenuMaster
{
    public class MenuMasterBI : IMenuMasterBI
    {
        private readonly IMenuMasterRepository _MenuMasterRepository;

        public MenuMasterBI(IMenuMasterRepository MenuMasterRepository)
        {
            this._MenuMasterRepository = MenuMasterRepository;
        }

        //public async Task<IEnumerable<DesignationMasterModel>> DesignationMasterList(int DesignationID)
        //{

        //    return await _DesignationMasterRepository.DesignationMasterList(DesignationID);
        //}

        public List<MenuMasterModel> MenuList()
        {
            return _MenuMasterRepository.MenuList();
        }
        public MenuMasterModel MenuRecord(int MenuId)
        {
            var keyValuePairs = _MenuMasterRepository.MenuRecord(MenuId);
            return keyValuePairs;
        }

        public MenuMasterModel SaveMenuRecord(MenuMasterModel ObjMenu)
        {
            var keyValuePairs = _MenuMasterRepository.SaveMenuRecord(ObjMenu);
            return keyValuePairs;
        }

        public MenuMasterModel DeleteMenuRecord(int UserID, int MenuId)
        {
            var keyValuePairs = _MenuMasterRepository.DeleteMenuRecord(UserID, MenuId);
            return keyValuePairs;
        }


    }
}



