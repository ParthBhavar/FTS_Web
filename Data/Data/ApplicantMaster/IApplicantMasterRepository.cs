using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COL.Model.Entities;
using Master.ViewModel;

namespace COL.Data.ApplicantMaster
{
    public interface IApplicantMasterRepository
    {
        ApplicantMasterModel SaveUserRegisterRecord(ApplicantMasterModel ObjReg);
    }
}
