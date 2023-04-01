using FTS.Data.Profile;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.Profile
{
    public class ProfileBI: IProfileBI
    {
        private readonly IProfileRepository _ProfileRepository;

        public ProfileBI(IProfileRepository ProfileRepository)
        {
            this._ProfileRepository = ProfileRepository;
        }


        public List<EmployeeMasterModel> ProfileList(int UserID)
        {
            return _ProfileRepository.ProfileList(UserID);
        }
        public EmployeeMasterModel ChangeEmployeePassword(EmployeeMasterModel ObjReglogin)
        {
            var keyValuePairs = _ProfileRepository.ChangeEmployeePassword(ObjReglogin);
            return keyValuePairs;
        }
    }


}
    