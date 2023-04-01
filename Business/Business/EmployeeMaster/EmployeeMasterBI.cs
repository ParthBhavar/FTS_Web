using FTS.Data.EmployeeMaster;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.EmployeeMaster
{
    public class EmployeeMasterBI : IEmployeeMasterBI
    {

        private readonly IEmployeeMasterRepository _EmployeeMasterRepository;

        public EmployeeMasterBI(IEmployeeMasterRepository EmployeeMasterRepositor)
        {
            this._EmployeeMasterRepository = EmployeeMasterRepositor;
        }

        //public async Task<IEnumerable<EmployeeMasterModel>> EmployeeMasterList(int EmployeeID)
        //{

        //    return await _EmployeeMasterRepository.EmployeeMasterList(EmployeeID);
        //}

        public List<EmployeeMasterModel> EmployeeMasterList()
        {
            var keyValuePairs = _EmployeeMasterRepository.EmployeeMasterList();
            return keyValuePairs;
        }


        public EmployeeMasterModel EmployeeRecord(int EmployeeID)
        {
            var keyValuePairs = _EmployeeMasterRepository.EmployeeRecord(EmployeeID);
            return keyValuePairs;
        }

        public EmployeeMasterModel SaveEmployeeRecord(EmployeeMasterModel ObjEmp)
        {
            var keyValuePairs = _EmployeeMasterRepository.SaveEmployeeRecord(ObjEmp);
            return keyValuePairs;
        }

        public EmployeeMasterModel DeleteEmployeeRecord(int UserID,int EmployeeID)
        {
            var keyValuePairs = _EmployeeMasterRepository.DeleteEmployeeRecord(UserID,EmployeeID);
            return keyValuePairs;
        }


    }
}
