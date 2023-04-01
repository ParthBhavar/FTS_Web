using COL.Data.Common;
using COL.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Data.RegistrationApplicationCLA
{
    public class RegistrationApplicationCLARepository : IRegistrationApplicationCLARepository
    {

        #region Private Variables
        private readonly IRepository<RegistrationApplicationCLAModel> _registrationapplicationCLARepository;
        #endregion

        #region Constructor
        public RegistrationApplicationCLARepository(IRepository<RegistrationApplicationCLAModel> registrationapplicationCLARepository)
        {
            _registrationapplicationCLARepository = registrationapplicationCLARepository;

        }
        #endregion

        public List<RegistrationApplicationCLAModel> RegistrationApplicationCLAList()
        {
            List<RegistrationApplicationCLAModel> lstRegistrationAppCLA = new List<RegistrationApplicationCLAModel>();
            DynamicParameters param = new DynamicParameters();
            var keyValuePairs = _registrationapplicationCLARepository.QueryMultipleByProcedure(SPConstants.GetListRegistrationApplicationCLA, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstRegistrationAppCLA = result1.Select(x => new RegistrationApplicationCLAModel
                {
                    RegistrationCLAID = (int)x.RegistrationCLAID,
                    EstablishmentName = (string)x.EstablishmentName,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstRegistrationAppCLA;
        }

        public RegistrationApplicationCLAModel RegistrationApplicationCLARecord(int RegistrationCLAID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_RegistrationCLAID", RegistrationCLAID);
            var keyValuePairs = _registrationapplicationCLARepository.QueryMultipleByProcedure(SPConstants.GetRecordRegistrationApplicationCLA, param);
            var response = new RegistrationApplicationCLAModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegistrationApplicationCLAModel
                {
                    RegistrationCLAID = (int)x.RegistrationCLAID,
                    EstablishmentName = (string)x.EstablishmentName,
                    EstablishmentAddress = (string)x.EstablishmentAddress,
                    DistrictID = (int)x.DistrictID,
                    TalukaID = (int)x.TalukaID,
                    AreaID = (int)x.AreaID,
                    Pincode = (int)x.Pincode,
                    FaxNo = (string)x.FaxNo,
                    PhoneNo = (int)x.PhoneNo,
                    Website = (string)x.Website,
                    MobileNo = (string)x.MobileNo,
                    EmailID = (string)x.EmailID,
                    PanCardNo = (string)x.PanCardNo,
                    BusinessNatureID = (int)x.BusinessNatureID,
                    TypeOfOwnershipID = (int)x.TypeOfOwnershipID,
                    CommencementDate = Convert.ToDateTime(x.CommencementDate),
                    EstablishmentTypeID = (int)x.EstablishmentTypeID,
                    TFActRegNo = (string)x.TFActRegNo,
                    TFActRegDate = Convert.ToDateTime(x.TFActRegDate),
                    TGSCEActRegNo = (string)x.TGSCEActRegNo,
                    TGSCEActRegDate = Convert.ToDateTime(x.TGSCEActRegDate),
                    EPFActRegNo = (string)x.EPFActRegNo,                   
                    EPFActRegDate = Convert.ToDateTime(x.EPFActRegDate),
                    ESIActRegNo = (string)x.ESIActRegNo,
                    ESIActRegDate = Convert.ToBoolean(x.ESIActRegDate),
                    PrincipalEmployerWorkerMale = (int)x.PrincipalEmployerWorkerMale,
                    PrincipalEmployerWorkerFemale = (int)x.PrincipalEmployerWorkerFemale,
                    PrincipalEmployerWorkerTotal = (int)x.PrincipalEmployerWorkerTotal,
                    ContractWorkerMale = (int)x.ContractWorkerMale,
                    ContractWorkerFemale = (int)x.ContractWorkerFemale,
                    ContractWorkerTotal = (int)x.ContractWorkerTotal,
                    EstablishmentContactPerson = (string)x.EstablishmentContactPerson,
                    EstablishmentContactPersonPhoneNo = (string)x.EstablishmentContactPersonPhoneNo,
                    EstablishmentContactPersonEmailID = (string)x.EstablishmentContactPersonEmailID,
                    PrincipalEmployerName = (string)x.PrincipalEmployerName,
                    PrincipalEmployerAddress = (string)x.PrincipalEmployerAddress,
                    PrincipalEmployerDesignationID = (int)x.PrincipalEmployerDesignationID,
                    PrincipalEmployerPhoneNo = (string)x.PrincipalEmployerPhoneNo,
                    PrincipalEmployerMobileNo = (string)x.PrincipalEmployerMobileNo,
                    PrincipalEmployerEmailID = (string)x.PrincipalEmployerEmailID,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }

        public RegistrationApplicationCLAModel SaveRegistrationApplicationCLA(RegistrationApplicationCLAModel Objregappclra)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_RegistrationCLAID", Objregappclra.RegistrationCLAID);
            param.Add("@p_EstablishmentName", Objregappclra.EstablishmentName);
            param.Add("@p_EstablishmentAddress", Objregappclra.EstablishmentAddress);
            param.Add("@p_DistrictID", Objregappclra.DistrictID);
            param.Add("@p_TalukaID", Objregappclra.TalukaID);
            param.Add("@p_AreaID", Objregappclra.AreaID);
            param.Add("@p_Pincode", Objregappclra.Pincode);
            param.Add("@p_FaxNo", Objregappclra.FaxNo);
            param.Add("@p_PhoneNo", Objregappclra.PhoneNo);
            param.Add("@p_Website", Objregappclra.Website);
            param.Add("@p_MobileNo", Objregappclra.MobileNo);
            param.Add("@p_EmailID", Objregappclra.EmailID);
            param.Add("@p_PanCardNo", Objregappclra.PanCardNo);
            param.Add("@p_BusinessNatureID", Objregappclra.BusinessNatureID);
            param.Add("@p_TypeOfOwnershipID", Objregappclra.TypeOfOwnershipID);
            param.Add("@p_CommencementDate", Objregappclra.CommencementDate);
            param.Add("@p_EstablishmentTypeID", Objregappclra.EstablishmentTypeID);
            param.Add("@p_TFActRegNo", Objregappclra.TFActRegNo);
            param.Add("@p_TFActRegDate", Objregappclra.TFActRegDate);
            param.Add("@p_TGSCEActRegNo", Objregappclra.TGSCEActRegNo);
            param.Add("@p_TGSCEActRegDate", Objregappclra.TGSCEActRegDate);
            param.Add("@p_EPFActRegNo", Objregappclra.EPFActRegNo);
            param.Add("@p_EPFActRegDate", Objregappclra.EPFActRegDate);
            param.Add("@p_ESIActRegNo", Objregappclra.ESIActRegNo);
            param.Add("@p_ESIActRegDate", Objregappclra.ESIActRegDate);
            param.Add("@p_PrincipalEmployerWorkerMale", Objregappclra.PrincipalEmployerWorkerMale);
            param.Add("@p_PrincipalEmployerWorkerFemale", Objregappclra.PrincipalEmployerWorkerFemale);
            param.Add("@p_PrincipalEmployerWorkerTotal", Objregappclra.PrincipalEmployerWorkerTotal);
            param.Add("@p_ContractWorkerMale", Objregappclra.ContractWorkerMale);
            param.Add("@p_ContractWorkerFemale", Objregappclra.ContractWorkerFemale);
            param.Add("@p_ContractWorkerTotal", Objregappclra.ContractWorkerTotal);
            param.Add("@p_EstablishmentContactPerson", Objregappclra.EstablishmentContactPerson);
            param.Add("@p_EstablishmentContactPersonPhoneNo", Objregappclra.EstablishmentContactPersonPhoneNo);
            param.Add("@p_EstablishmentContactPersonEmailID", Objregappclra.EstablishmentContactPersonEmailID);
            param.Add("@p_PrincipalEmployerName", Objregappclra.PrincipalEmployerName);
            param.Add("@p_PrincipalEmployerDesignationID", Objregappclra.PrincipalEmployerDesignationID);
            param.Add("@p_PrincipalEmployerPhoneNo", Objregappclra.PrincipalEmployerPhoneNo);
            param.Add("@p_PrincipalEmployerMobileNo", Objregappclra.PrincipalEmployerMobileNo);
            param.Add("@p_PrincipalEmployerEmailID", Objregappclra.PrincipalEmployerEmailID);           
            param.Add("@p_IsActive", Objregappclra.IsActive);            
            param.Add("@p_IsDeleted", Objregappclra.IsDeleted);
            var keyValuePairs = _registrationapplicationCLARepository.QueryMultipleByProcedure(SPConstants.UpdateRegistrationApplicationCLA, param);
            var response = new RegistrationApplicationCLAModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new RegistrationApplicationCLAModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
    }
}
