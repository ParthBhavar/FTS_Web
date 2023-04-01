using COL.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COL.Business.RegistrationApplicationCLA
{
    public interface IRegistrationApplicationCLABI
    {
        List<RegistrationApplicationCLAModel> RegistrationApplicationCLAList();
        RegistrationApplicationCLAModel RegistrationApplicationCLARecord(int RegistrationCLAID);
        RegistrationApplicationCLAModel SaveRegistrationApplicationCLA(RegistrationApplicationCLAModel Objregappclra);
    }
}
