using FTS.Data.EmployeeLoginMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.EmployeeLoginMaster
{
    public class EmployeeLoginMasterBI : IEmployeeLoginMasterBI
    {
        private readonly IEmployeeLoginMasterRepository _IEmployeeLoginMasterRepository;
            public EmployeeLoginMasterBI(IEmployeeLoginMasterRepository EmployeeLoginMasterRepositor)
            {
                this._IEmployeeLoginMasterRepository = EmployeeLoginMasterRepositor;
            }
            public EmployeeMasterModel EmployeeLoginRecord(EmployeeMasterModel ObjReglogin)
            {
                var keyValuePairs = _IEmployeeLoginMasterRepository.EmployeeLoginRecord(ObjReglogin);
                return keyValuePairs;
            }
        public EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            var keyValuePairs = _IEmployeeLoginMasterRepository.ChangeEmployeePassword(ObjReglogin);
            return keyValuePairs;
        }


        public List<MenuListModel> MenuList(EmployeeMasterModel Obj)
        {
            var keyValuePairs = _IEmployeeLoginMasterRepository.MenuList(Obj);
            return keyValuePairs;
        }
        public List<UserPositionListModel> UserPositionList(EmployeeMasterModel Obj)
        {
            var keyValuePairs = _IEmployeeLoginMasterRepository.UserPositionList(Obj);
            return keyValuePairs;
        }

        public EmployeeMasterModel UpdateIslockedflage(EmployeeMasterModel Obj)
        {
            var keyValuePairs = _IEmployeeLoginMasterRepository.UpdateIslockedflage(Obj);
            return keyValuePairs;
        }


    }
}
