using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.DesignationMaster
{
    public class DesignationMasterRepository : IDesignationMasterRepository
    {
        #region Private Variables
        private readonly IRepository<DesignationMasterModel> _designationmasterRepository;
        #endregion

        #region Constructor
        public DesignationMasterRepository(IRepository<DesignationMasterModel> designationmasterRepository)
        {
            _designationmasterRepository = designationmasterRepository;
            #endregion;

        }
        IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());

        public List<DesignationMasterModel> DesignationList()
        {
            //var _ID = HttpContext.Session.GetInt32("_ID");
            //var _UserMode = HttpContext.Session.GetInt32("_UserMode");
            var IP = heserver.AddressList[1].ToString();
            //if (model.SearchText == null)
            //{
            //    model.SearchText = "";
            //}
            try
            {
                List<DesignationMasterModel> lstDesignationMaster = new List<DesignationMasterModel>();
                DynamicParameters param = new DynamicParameters();


                var keyValuePairs = _designationmasterRepository.QueryMultipleByProcedure(SPConstants.GetListDesignationMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstDesignationMaster = result1.Select(x => new DesignationMasterModel
                    {
                        DesignationID = (int)x.DesignationID,
                        DesignationName = (string)x.DesignationName,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).ToList();
                };
                return lstDesignationMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //public async Task<IEnumerable<DesignationMasterModel>> DesignationMasterList(int DesignationID)
        //{

        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@p_DesignationID", DesignationID);
        //    var keyValuePairs = await _designationmasterRepository.QueryMultipleByProcedureAsync(SPConstants.GetListDesignationMaster, param);
        //    return await _designationmasterRepository.QueryListByProcedureAsync(SPConstants.GetListDesignationMaster, param);

        //}






        public DesignationMasterModel DesignationRecord(int DesignationID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DesignationID", DesignationID);
                var keyValuePairs = _designationmasterRepository.QueryMultipleByProcedure(SPConstants.GetRecoredDesignationMaster, param);
                var response = new DesignationMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DesignationMasterModel
                    {
                        DesignationID = (int)x.DesignationID,
                        DesignationName = (string)x.DesignationName,
                        IsActive = Convert.ToBoolean(x.IsActive),
                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DesignationMasterModel SaveDesignationRecord(DesignationMasterModel ObjDes)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_DesignationID", ObjDes.DesignationID);
                param.Add("@p_DesignationName", ObjDes.DesignationName);
                param.Add("@p_IsActive", ObjDes.IsActive);
                param.Add("@p_IsDeleted", ObjDes.IsDeleted);
                var keyValuePairs = _designationmasterRepository.QueryMultipleByProcedure(SPConstants.updateDesignationMaster, param);
                var response = new DesignationMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DesignationMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DesignationMasterModel DeleteDesignationRecord(int UserID,int DesignationID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_DesignationID", DesignationID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _designationmasterRepository.QueryMultipleByProcedure(SPConstants.DeleteDesignationMaster, param);
                var response = new DesignationMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new DesignationMasterModel
                    {
                        ErrorCode = (int)x.ErrorCode,
                        ErrorMassage = (string)x.ErrorMassage,
                        //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
