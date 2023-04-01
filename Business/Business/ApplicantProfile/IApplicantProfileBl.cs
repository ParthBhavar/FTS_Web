using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ApplicantProfile
{
    public interface IApplicantProfileBl
    {
        //ApplicantMasterModel ApplicantProfileRecord(int ApplicantID);
        ApplicantMasterModel SaveuserRecord(ApplicantMasterModel Objreg);
        ApplicantMasterModel GetuserRecord(int ApplicantID);
        ApplicantMasterModel ChangeApplicantPassword(ApplicantMasterModel Objreg);
    }
}
