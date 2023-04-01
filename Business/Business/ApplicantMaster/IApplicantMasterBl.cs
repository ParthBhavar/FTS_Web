using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COL.Model.Entities;
using Master.ViewModel;

namespace COL.Business.ApplicantMaster
{
    public interface IApplicantMasterBl
    {
        ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg);
    }
}
