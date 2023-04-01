using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EstablishmentMaster
{

    
        
    public class EstablishmentMasterRepository: IEstablishmentMasterRepository
    {

        #region Private Variables
        private readonly IRepository<EstablishmentMasterModel> _establishmentmasterRepository;
        #endregion

        #region Constructor
        public EstablishmentMasterRepository(IRepository<EstablishmentMasterModel> establishmentmasterRepository)
        {
            _establishmentmasterRepository = establishmentmasterRepository;
            #endregion;

        }

        #region ALL LIST

        public List<EstablishmentMasterModel> EstablishmentMasterList()
        {

            List<EstablishmentMasterModel> lstEstablishmentMaster = new List<EstablishmentMasterModel>();
            DynamicParameters param = new DynamicParameters();


            var keyValuePairs = _establishmentmasterRepository.QueryMultipleByProcedure(SPConstants.GetListEstablishmentMaster, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstEstablishmentMaster = result1.Select(x => new EstablishmentMasterModel
                {
                    EstablishmentID = (int)x.EstablishmentID,
                    EstablishmentCode = x.EstablishmentCode,
                    EstablishmentName = x.EstablishmentName,
                    PAddress = x.PAddress,
                    SAddress = x.SAddress,
                    TalukaID = (int)x.TalukaID,
                    DistrictID = (int)x.DistrictID,
                    Pincode = (int)x.Pincode,
                    IsActive = Convert.ToBoolean(x.IsActive)
                }).ToList();
            };
            return lstEstablishmentMaster;
        }
        #endregion




        #region GET RECORED

        public EstablishmentMasterModel EstablishmentRecord(int EstablishmentID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EstablishmentID", EstablishmentID);
            var keyValuePairs = _establishmentmasterRepository.QueryMultipleByProcedure(SPConstants.GetRecordEstablishmentMaster, param);
            var response = new EstablishmentMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EstablishmentMasterModel
                {
                    EstablishmentID = (int)x.EstablishmentID,
                    EstablishmentCode = x.EstablishmentCode,
                    EstablishmentName = x.EstablishmentName,
                    PAddress = x.PAddress,
                    SAddress = x.SAddress,
                    TalukaID = (int)x.TalukaID,
                    DistrictID = (int)x.DistrictID,
                    Pincode = (int)x.Pincode,
                    //CreatedOn = x.CreatedOn,
                    //ModifiedOn = x.ModifiedOn,
                    //ModifiedBy = x.ModifiedBy,
                    //DeletedOn = x.DeletedOn,
                    //DeletedBy = x.DeletedBy,
                    IsActive = Convert.ToBoolean(x.IsActive)

                }).FirstOrDefault();
            };
            return response;
        }
        #endregion




        #region SAVE RECORED
        public EstablishmentMasterModel SaveEstablishmentRecord(EstablishmentMasterModel Objest)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EstablishmentID", Objest.EstablishmentID);
            param.Add("@p_EstablishmentCode", Objest.EstablishmentCode);
            param.Add("@p_EstablishmentName", Objest.EstablishmentName);
            param.Add("@p_PAddress", Objest.PAddress);
            param.Add("@p_SAddress", Objest.SAddress);
            param.Add("@p_TalukaID", Objest.TalukaID);
            param.Add("@p_DistrictID", Objest.DistrictID);
            param.Add("@p_Pincode", Objest.Pincode);
            param.Add("@p_UserID", 1);
            //param.Add("@p_CreatedOn", Objest.CreatedOn);
            //param.Add("@p_ModifiedOn", Objest.ModifiedOn);
            //param.Add("@p_ModifiedBy", Objest.ModifiedBy);
            //param.Add("@p_DeletedOn", Objest.DeletedOn);
            //param.Add("@p_DeletedBy", Objest.DeletedBy);
            param.Add("@p_IsActive", Objest.IsActive);
           // param.Add("@p_IsDelete", Objest.IsDelete);
            var keyValuePairs = _establishmentmasterRepository.QueryMultipleByProcedure(SPConstants.UpdateEstablishmentMaster, param);
            var response = new EstablishmentMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EstablishmentMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        #endregion




        #region DELETE RECORED

        public EstablishmentMasterModel DeleteEstablishmentRecord(int EstablishmentID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_EstablishmentID", EstablishmentID);
            var keyValuePairs = _establishmentmasterRepository.QueryMultipleByProcedure(SPConstants.DeleteEstablishmentMaster, param);
            var response = new EstablishmentMasterModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new EstablishmentMasterModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    //IsActive = Convert.ToBoolean(x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
        #endregion




    }
}
