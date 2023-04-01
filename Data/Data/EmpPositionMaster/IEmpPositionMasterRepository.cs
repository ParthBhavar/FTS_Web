﻿using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.EmpPositionMaster
{
    public interface IEmpPositionMasterRepository
    {
        List<EmpPositionMasterModel> EmpPositionMasterList();
        EmpPositionMasterModel EmpPositionMasterRecord(int EmpPosID);
        EmpPositionMasterModel SaveEmpPositionMasterRecord(EmpPositionMasterModel ObjEmpPosition);
        EmpPositionMasterModel DeleteEmpPositionMasterRecord(int UserID, int EmpPosID);
        EmpPositionMasterModel SaveAssignPositionRecord(EmpPositionMasterModel ObjEmpPosition);
    }
}
