using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Profile
{
    public interface IProfileBI
    {
        List<EmployeeMasterModel> ProfileList(int UserID);
        EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin);
    }
}
