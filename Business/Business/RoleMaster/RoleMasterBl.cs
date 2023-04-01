using FTS.Data.RoleMaster;
using FTS.Model;
using FTS.Model.Common;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.RoleMaster
{
    public class RoleMasterBl : IRoleMasterBl
    {
        private readonly IRoleMasterRepository _RoleMasterRepository;

        public RoleMasterBl(IRoleMasterRepository RoleMasterRepositor)
        {
            this._RoleMasterRepository = RoleMasterRepositor;
        }

        //public List<MRoleMaster> RoleList(PaginationRequest model)
        //{
        //    return  _RoleMasterRepository.RoleList(model);
        //}

        public List<MRoleMaster> RoleList()
        {
            return _RoleMasterRepository.RoleList();
        }

        public MRoleMaster RoleRecord(int RoleID)
        {
            var keyValuePairs = _RoleMasterRepository.RoleRecord(RoleID);
            return keyValuePairs;
        }

        public MRoleMaster SaveRoleRecord(MRoleMaster ObjROle)
        {
            var keyValuePairs = _RoleMasterRepository.SaveRoleRecord(ObjROle);
            return keyValuePairs;
        }

        public MRoleMaster DeleteRoleRecord(int UserID, int RoleID)
        {
            var keyValuePairs = _RoleMasterRepository.DeleteRoleRecord(UserID,RoleID);
            return keyValuePairs;
        }
    }

}
