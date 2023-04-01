using FTS.Data.RegistrationCLRA;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.RegistrationCLRA
{
    public class RegistrationCLRABl : IRegistrationCLRABl
    {
        private readonly IRegistrationCLRARepository _RegistrationCLRARepository;

        public RegistrationCLRABl(IRegistrationCLRARepository RegistrationCLRARepositor)
        {
            this._RegistrationCLRARepository = RegistrationCLRARepositor;
        }
        public List<RegistrationCLRAModel> RegistrationCLRAList()
        {
            return _RegistrationCLRARepository.RegistrationCLRAList();
        }
        public RegistrationCLRAModel RegistrationCLRARecord(int RegistrationID)
        {
            var keyValuePairs = _RegistrationCLRARepository.RegistrationCLRARecord(RegistrationID);
            return keyValuePairs;
        }
        public RegistrationCLRAModel SaveRegistrationCLRA(RegistrationCLRAModel Objregclra)
        {
            var keyValuePairs = _RegistrationCLRARepository.SaveRegistrationCLRA(Objregclra);
            return keyValuePairs;
        }
    }
}
