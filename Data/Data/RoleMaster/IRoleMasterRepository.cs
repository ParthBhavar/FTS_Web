using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTS.Model;
using FTS.Model.Common;
using FTS.Model.Entities;

namespace FTS.Data.RoleMaster
{
    public interface IRoleMasterRepository
    {
        //List<MRoleMaster> RoleList(PaginationRequest model);
        List<MRoleMaster> RoleList();
        MRoleMaster RoleRecord(int RoleID);
        MRoleMaster SaveRoleRecord(MRoleMaster ObjROle);
        MRoleMaster DeleteRoleRecord(int UserID, int RoleID);
    }
}
