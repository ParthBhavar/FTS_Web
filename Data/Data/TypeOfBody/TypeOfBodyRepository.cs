using FTS.Data.Common;
using FTS.Model.Entities;
using Dapper;
using Data.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TypeOfBody
{
    public class TypeOfBodyRepository : ITypeOfBodyRepository
    {

        #region Private Variables
        private readonly IRepository<TypeOfBodyModel> _typeofbodyRepository;
        #endregion

        #region Constructor
        public TypeOfBodyRepository(IRepository<TypeOfBodyModel> typeofbodyRepository)
        {
            _typeofbodyRepository = typeofbodyRepository;

        }
        #endregion


        public List<TypeOfBodyModel> TypeOfBodyList()
        {
            List<TypeOfBodyModel> lstTypeOfBody = new List<TypeOfBodyModel>();
            DynamicParameters param = new DynamicParameters();

            var keyValuePairs = _typeofbodyRepository.QueryMultipleByProcedure(SPConstants.GetListTypeOfBody, param);

            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                lstTypeOfBody = result1.Select(x => new TypeOfBodyModel
                {
                    TypeOfBodyID = (int)x.TypeOfBodyID,
                    Authority = (string)x.Authority,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).ToList();
            };
            return lstTypeOfBody;
        }

        public TypeOfBodyModel TypeOfBodyRecord(int TypeOfBodyID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_TypeOfBodyID", TypeOfBodyID);
            var keyValuePairs = _typeofbodyRepository.QueryMultipleByProcedure(SPConstants.GetRecordTypeOfBody, param);
            var response = new TypeOfBodyModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TypeOfBodyModel
                {
                    TypeOfBodyID = (int)x.TypeOfBodyID,
                    Authority = (string)x.Authority,
                    IsActive = Convert.ToBoolean(x.IsActive),
                }).FirstOrDefault();
            };
            return response;
        }
        public TypeOfBodyModel SaveTypeOfBodyRecord(TypeOfBodyModel Objtypeofbody)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_UserID", 1);
            param.Add("@p_TypeOfBodyID", Objtypeofbody.TypeOfBodyID);
            param.Add("@p_Authority", Objtypeofbody.Authority);
            param.Add("@p_IsActive", Objtypeofbody.IsActive);
            param.Add("@p_IsDeleted", Objtypeofbody.IsDeleted);
            var keyValuePairs = _typeofbodyRepository.QueryMultipleByProcedure(SPConstants.UpdateTypeOfBody, param);
            var response = new TypeOfBodyModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TypeOfBodyModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }

        public TypeOfBodyModel DeleteTypeOfBody(int TypeOfBodyID, int UserID)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@p_TypeOfBodyID", TypeOfBodyID);
            param.Add("@p_UserID", UserID);
            var keyValuePairs = _typeofbodyRepository.QueryMultipleByProcedure(SPConstants.DeleteTypeOfBody, param);
            var response = new TypeOfBodyModel();
            if (keyValuePairs["result1"] is IEnumerable<dynamic> result1 && result1.Any())
            {
                response = result1.Select(x => new TypeOfBodyModel
                {
                    ErrorCode = (int)x.ErrorCode,
                    ErrorMassage = (string)x.ErrorMassage,
                    // IsActive = Convert.ToBoolean((int)x.IsActive).ToString(),

                }).FirstOrDefault();
            };
            return response;
        }
    }
}
