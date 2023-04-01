using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.RegistrationCLRA
{
    public class RegistrationCLRARepository : IRegistrationCLRARepository
    {
        #region Private Variables
        private readonly IRepository<RegistrationCLRAModel> _registrationCLRARepository;
        #endregion

        #region Constructor
        public RegistrationCLRARepository(IRepository<RegistrationCLRAModel> registrationCLRARepository)
        {
            _registrationCLRARepository = registrationCLRARepository;

        }
        #endregion

        public List<RegistrationCLRAModel> RegistrationCLRAList()
        {
            List<RegistrationCLRAModel> lstProjectDetails = new List<RegistrationCLRAModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _registrationCLRARepository.QueryMultipleByProcedure(SPConstants.GetListRegistrationCLRA, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstProjectDetails = result1.Select(x => new RegistrationCLRAModel
                {
                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.EstablishmentName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstProjectDetails;
        }
        public RegistrationCLRAModel RegistrationCLRARecord(int RegistrationID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_RegistrationID", RegistrationID);
            var keyValuePairs = _registrationCLRARepository.QueryMultipleByProcedure(SPConstants.GetRecordRegistrationCLRA, param);
            var response = new RegistrationCLRAModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegistrationCLRAModel
                {
                    RegistrationID = (int)x.RegistrationID,
                    EstablishmentName = (string)x.EstablishmentName,
                    EstablishmentAddress = (string)x.EstablishmentAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    Pincode = (int)x.Pincode,
                    TypeOfBusinessTrade = (string)x.TypeOfBusinessTrade,
                    PrincipalEmployerName = (string)x.PrincipalEmployerName,
                    FatherName = (string)x.FatherName,
                    PrincipalEmployerAddress = (string)x.PrincipalEmployerAddress,
                    EmpDistrictID = (int)x.EmpDistrictID,
                    EmpTalukaID = (int)x.EmpTalukaID,
                    EmpPincode = (int)x.EmpPincode,
                    ContractorName = (string)x.ContractorName,
                    ContractorAddress = (string)x.ContractorAddress,
                    NatureOfWork = (string)x.NatureOfWork,
                    MaxNoContLab = (int)x.MaxNoContLab,
                    EstimateddateofCommencement = Convert.ToDateTime(x.EstimateddateofCommencement),
                    EstimatedDateOfCompletion = Convert.ToDateTime(x.EstimatedDateOfCompletion),
                    RegistrationFees = (int)x.RegistrationFees,
                    Treasury = (string)x.Treasury,
                    ChallanNumber = (string)x.ChallanNumber,
                    ChallanDate = Convert.ToDateTime(x.ChallanDate),
                    Declaration = Convert.ToBoolean(x.Declaration),
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public RegistrationCLRAModel SaveRegistrationCLRA(RegistrationCLRAModel Objregclra)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_RegistrationID", Objregclra.RegistrationID);
            param.Add("@p_EstablishmentName", Objregclra.EstablishmentName);
            param.Add("@p_EstablishmentAddress", Objregclra.EstablishmentAddress);
            param.Add("@p_DistrictID", Objregclra.DistrictID);
            param.Add("@p_TalukaID", Objregclra.TalukaID);
            param.Add("@p_Pincode", Objregclra.Pincode);
            param.Add("@p_TypeOfBusinessTrade", Objregclra.TypeOfBusinessTrade);
            param.Add("@p_PrincipalEmployerName", Objregclra.PrincipalEmployerName);
            param.Add("@p_FatherName", Objregclra.FatherName);
            param.Add("@p_PrincipalEmployerAddress", Objregclra.PrincipalEmployerAddress);
            param.Add("@p_EmpDistrictID", Objregclra.EmpDistrictID);
            param.Add("@p_EmpTalukaID", Objregclra.EmpTalukaID);
            param.Add("@p_EmpPincode", Objregclra.EmpPincode);
            param.Add("@p_ContractorName", Objregclra.ContractorName);
            param.Add("@p_ContractorAddress", Objregclra.ContractorAddress);
            param.Add("@p_NatureOfWork", Objregclra.NatureOfWork);
            param.Add("@p_MaxNoContLab", Objregclra.MaxNoContLab);
            param.Add("@p_EstimateddateofCommencement", Objregclra.EstimateddateofCommencement);
            param.Add("@p_EstimatedDateOfCompletion", Objregclra.EstimatedDateOfCompletion);
            param.Add("@p_RegistrationFees", Objregclra.RegistrationFees);
            param.Add("@p_Treasury", Objregclra.Treasury);
            param.Add("@p_ChallanNumber", Objregclra.ChallanNumber);
            param.Add("@p_ChallanDate", Objregclra.ChallanDate);
            param.Add("@p_Declaration", Objregclra.Declaration);
            param.Add("@p_IsActive", Objregclra.IsActive);
            param.Add("@p_IsDeleted", Objregclra.IsDeleted);
            var keyValuePairs = _registrationCLRARepository.QueryMultipleByProcedure(SPConstants.UpdateRegistrationCLRA, param);
            var response = new RegistrationCLRAModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegistrationCLRAModel
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
