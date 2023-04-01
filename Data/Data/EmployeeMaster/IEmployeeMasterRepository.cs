using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmployeeMaster
{
    public interface IEmployeeMasterRepository
    {
        //Task<IEnumerable<EmployeeMasterModel>> EmployeeMasterList(int EmployeeID);

        List<EmployeeMasterModel> EmployeeMasterList();

        EmployeeMasterModel EmployeeRecord(int EmployeeID);

        EmployeeMasterModel SaveEmployeeRecord(EmployeeMasterModel ObjEmp);

        EmployeeMasterModel DeleteEmployeeRecord(int UserID,int EmployeeID);
    }
}
