using COL.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Data.RegistrationApplicationCLA
{
    public interface IRegistrationApplicationCLARepository
    {
        List<RegistrationApplicationCLAModel> RegistrationApplicationCLAList();
        RegistrationApplicationCLAModel RegistrationApplicationCLARecord(int RegistrationCLAID);
        RegistrationApplicationCLAModel SaveRegistrationApplicationCLA(RegistrationApplicationCLAModel Objregappclra);
    }
}
