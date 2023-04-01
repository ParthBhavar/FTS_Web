using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplicantLoginMaster
{
    public interface IApplicantLoginMasterRepository
    {
        ApplicantMasterModel SaveApplicantLoginRecord(ApplicantMasterModel ObjReglogin);
    }
}
