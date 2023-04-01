using FTS.Data.Common;
using FTS.Model.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmployeeMaster
{
    public class EmployeeMasterRepository : IEmployeeMasterRepository
    {
   
        #region Private Variables
        private readonly IRepository<EmployeeMasterModel> _employeemasterRepository;
        #endregion

        #region Constructor
        public EmployeeMasterRepository(IRepository<EmployeeMasterModel> employeemasterRepository)
        {
            _employeemasterRepository = employeemasterRepository;
            #endregion;

        }


        public List<EmployeeMasterModel> EmployeeMasterList()
        {
            try
            {

                List<EmployeeMasterModel> lstEmployeeMaster = new List<EmployeeMasterModel>();
                DynamicParameters param = new DynamicParameters();


                var keyValuePairs = _employeemasterRepository.QueryMultipleByProcedure(SPConstants.GetListEmployeeMaster, param);

                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    lstEmployeeMaster = result1.Select(x => new EmployeeMasterModel
                    {
                        EmployeeID = (int)x.EmployeeID,
                        EmpCode = x.EmpCode,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        EmailID = x.EmailID,
                        MobileNo = x.MobileNo,
                        Gender = (int)x.Gender,
                        IsActive = Convert.ToBoolean(x.IsActive)
                    }).ToList();
                };
                return lstEmployeeMaster;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task<IEnumerable<EmployeeMasterModel>> EmployeeMasterList(int EmployeeID)
        //{

        //    DynamicParameters param = new DynamicParameters();
        //    param.Add("@p_EmployeeID", EmployeeID);
        //    var keyValuePairs = await _employeemasterRepository.QueryMultipleByProcedureAsync(SPConstants.GetListEmployeeMaster, param);
        //    return await _employeemasterRepository.QueryListByProcedureAsync(SPConstants.GetListEmployeeMaster, param);

        //}






        public EmployeeMasterModel EmployeeRecord(int EmployeeID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", EmployeeID);
                var keyValuePairs = _employeemasterRepository.QueryMultipleByProcedure(SPConstants.GetRecordEmployeeMaster, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
                    {
                        EmployeeID = (int)x.EmployeeID,
                        EmpCode = x.EmpCode,
                        DesignationID = (int)x.DesignationID,
                        RegionID = (int)x.RegionID,
                        FirstName = x.FirstName,
                        MiddleName = x.MiddleName,
                        LastName = x.LastName,
                        EmailID = x.EmailID,
                        MobileNo = x.MobileNo,
                        PAddress = x.PAddress,
                        SAddress = x.SAddress,
                        Gender = (int)x.Gender,
                        DOB = Convert.ToDateTime(x.DOB),
                        //DOB = (DateTime)x.DOB,
                        //DOB = x.DOB,
                        Password =  Encrypt_Decrypt.Decrypt(x.Password),
                        IsActive = Convert.ToBoolean(x.IsActive)
                    }).FirstOrDefault();
                };
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public EmployeeMasterModel SaveEmployeeRecord(EmployeeMasterModel ObjEmp)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_UserID", 1);
                param.Add("@p_EmployeeID", ObjEmp.EmployeeID);
                param.Add("@p_EmpCode", ObjEmp.EmpCode);
                param.Add("@p_DesignationID", ObjEmp.DesignationID);
                param.Add("@p_RegionID", ObjEmp.RegionID);
                param.Add("@p_FirstName", ObjEmp.FirstName);
                param.Add("@p_MiddleName", ObjEmp.MiddleName);
                param.Add("@p_LastName", ObjEmp.LastName);
                param.Add("@p_EmailID", ObjEmp.EmailID);
                param.Add("@p_MobileNo", ObjEmp.MobileNo);
                param.Add("@p_PAddress", ObjEmp.PAddress);
                param.Add("@p_SAddress", ObjEmp.SAddress);
                param.Add("@p_Gender", ObjEmp.Gender);
                param.Add("@p_DOB", ObjEmp.UPDOB);
                param.Add("@p_Password", ObjEmp.Password);
                //param.Add("@p_Password", Encrypt_Decrypt.Encrypt(ObjEmp.Password));
                param.Add("@p_IsActive", ObjEmp.IsActive);
                var keyValuePairs = _employeemasterRepository.QueryMultipleByProcedure(SPConstants.UpdateEmployeeMaster, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
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


        public EmployeeMasterModel DeleteEmployeeRecord(int UserID,int EmployeeID)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@p_EmployeeID", EmployeeID);
                param.Add("@p_UserID", UserID);
                var keyValuePairs = _employeemasterRepository.QueryMultipleByProcedure(SPConstants.DeleteEmployeeMaster, param);
                var response = new EmployeeMasterModel();
                if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
                {
                    response = result1.Select(x => new EmployeeMasterModel
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

