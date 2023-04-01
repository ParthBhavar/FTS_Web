using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.Profile
{
    public interface IProfileRepository
    {
        List<EmployeeMasterModel> ProfileList(int UserID);

        EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin);
    }
}
