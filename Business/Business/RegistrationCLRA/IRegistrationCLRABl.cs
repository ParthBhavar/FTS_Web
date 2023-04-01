using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.RegistrationCLRA
{
    public interface IRegistrationCLRABl
    {
        List<RegistrationCLRAModel> RegistrationCLRAList();
        RegistrationCLRAModel RegistrationCLRARecord(int RegistrationID);
        RegistrationCLRAModel SaveRegistrationCLRA(RegistrationCLRAModel Objregclra);

    }
}
