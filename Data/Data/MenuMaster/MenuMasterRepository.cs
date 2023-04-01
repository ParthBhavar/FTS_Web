using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.MenuMaster
{
     public class MenuMasterRepository : IMenuMasterRepository
    {
        #region Private Variables
        private readonly IRepository<MenuMasterModel> _menumasterRepository;
        #endregion

        #region Constructor
        public MenuMasterRepository(IRepository<MenuMasterModel> menumasterRepository)
        {
            _menumasterRepository = menumasterRepository;
            #endregion;

        }


        public List<MenuMasterModel> MenuList()
        {
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            List<MenuMasterModel> lstMenuMaster = new List<MenuMasterModel>();
            DynamicParameters param = new DynamicParameters();


            var keyValuePairs = _menumasterRepository.QueryMultipleByProcedure(SPConstants.GetListMenuMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstMenuMaster = result1.Select(x => new MenuMasterModel
                {
                    MenuId = x.MenuId,
                    //ParentMenuId = x.ParentMenuId,
                    MenuName = (string)x.MenuName,
                    MenuDescription = (string)x.MenuDescription,
                    PageURL = (string)x.pageURL,
                    Icon = (string)x.Icon,
                    //NodeLevel = (int)x.NodeLevel,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstMenuMaster;
        }

        public MenuMasterModel MenuRecord(int MenuId)
        {
            List<MCommonList> AssignMenus = new List<MCommonList>();
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_MenuId", MenuId);
            var keyValuePairs = _menumasterRepository.QueryMultipleByProcedure(SPConstants.GetRecordMenuMaster, param);
            var response = new MenuMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new MenuMasterModel
                {
                    MenuId = (int)x.MenuId,
                    ParentMenuId = (int)x.ParentMenuId,
                    MenuName = (string)x.MenuName,
                    MenuDescription = (string)x.MenuDescription,
                    PageName = (string)x.PageName,
                    PageURL = (string)x.PageURL,
                    Icon = (string)x.Icon,
                    NodeLevel = (int)x.NodeLevel,
                    IsAssign = (int)x.IsAssign,
                    IsActive = Convert.ToBoolean(x.IsActive),
                   
                }).FirstOrDefault();
            };

            if (keyValuePairs["result2"] is IEnumerable<dynamic> result2 && result2.Any())
            {
                AssignMenus = result2.Select(x => new MCommonList
                {
                    DataValue = (int)x.DataValue,
                    DisplayValue = (string)x.DisplayValue,

                }).ToList();
            };
            response.AssignMenuList = AssignMenus;
            return response;
        }


        public MenuMasterModel SaveMenuRecord(MenuMasterModel ObjMenu)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", ObjMenu.UserID);
            param.Add("@p_MenuId", ObjMenu.MenuId);
            param.Add("@p_ParentMenuId", ObjMenu.ParentMenuId);
            param.Add("@p_MenuName", ObjMenu.MenuName);
            param.Add("@p_MenuDescription", ObjMenu.MenuDescription);
            param.Add("@p_PageName", ObjMenu.PageName);
            param.Add("@p_PageURL", ObjMenu.PageURL);
            param.Add("@p_Icon", ObjMenu.Icon);
            param.Add("@p_NodeLevel", ObjMenu.NodeLevel);
            param.Add("@p_IsActive", ObjMenu.IsActive);
            param.Add("@p_IsAssign", ObjMenu.IsAssign);
            var keyValuePairs = _menumasterRepository.QueryMultipleByProcedure(SPConstants.UpdateMenuMaster, param);
            var response = new MenuMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new MenuMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }


        public MenuMasterModel DeleteMenuRecord(int UserID, int MenuId)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_MenuId", MenuId);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _menumasterRepository.QueryMultipleByProcedure(SPConstants.DeleteMenuMaster, param);
            var response = new MenuMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new MenuMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }


    }
}



