using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.EstablishmentMaster
{
    public interface IEstablishmentMasterBI
    {
        List<EstablishmentMasterModel> EstablishmentMasterList();
        EstablishmentMasterModel EstablishmentRecord(int EstablishmentID);
        EstablishmentMasterModel SaveEstablishmentRecord(EstablishmentMasterModel Objest);
        EstablishmentMasterModel DeleteEstablishmentRecord(int EstablishmentID);
       

    }
}
