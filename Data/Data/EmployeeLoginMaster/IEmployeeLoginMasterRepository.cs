﻿using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmployeeLoginMaster
{
    public interface IEmployeeLoginMasterRepository
    {
        public EmployeeMasterModel EmployeeLoginRecord(EmployeeMasterModel ObjReglogin);
        public EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin);
        public List<MenuListModel> MenuList(EmployeeMasterModel Obj);
        public List<UserPositionListModel> UserPositionList(EmployeeMasterModel Obj);
        public EmployeeMasterModel UpdateIslockedflage(EmployeeMasterModel Obj);
    }
}
