using COL.Data.RegistrationApplicationCLA;
using COL.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Business.RegistrationApplicationCLA
{
    public class RegistrationApplicationCLABI : IRegistrationApplicationCLABI
    {
        private readonly IRegistrationApplicationCLARepository _RegistrationApplicationCLARepository;

        public RegistrationApplicationCLABI(IRegistrationApplicationCLARepository RegistrationApplicationCLARepositor)
        {
            this._RegistrationApplicationCLARepository = RegistrationApplicationCLARepositor;
        }
        public List<RegistrationApplicationCLAModel> RegistrationApplicationCLAList()
        {
            return _RegistrationApplicationCLARepository.RegistrationApplicationCLAList();
        }
        public RegistrationApplicationCLAModel RegistrationApplicationCLARecord(int RegistrationCLAID)
        {
            var keyValuePairs = _RegistrationApplicationCLARepository.RegistrationApplicationCLARecord(RegistrationCLAID);
            return keyValuePairs;
        }
        public RegistrationApplicationCLAModel SaveRegistrationApplicationCLA(RegistrationApplicationCLAModel Objregappclra)
        {
            var keyValuePairs = _RegistrationApplicationCLARepository.SaveRegistrationApplicationCLA(Objregappclra);
            return keyValuePairs;
        }

    }
}
