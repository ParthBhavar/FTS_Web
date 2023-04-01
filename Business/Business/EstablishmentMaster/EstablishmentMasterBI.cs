using FTS.Data.EstablishmentMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.EstablishmentMaster
{
    
        public class EstablishmentMasterBI : IEstablishmentMasterBI
    {
            private readonly IEstablishmentMasterRepository _EstablishmentMasterRepository;

            public EstablishmentMasterBI(IEstablishmentMasterRepository EstablishmentMasterRepositor)
            {
                this._EstablishmentMasterRepository = EstablishmentMasterRepositor;
            }

           

            public List<EstablishmentMasterModel> EstablishmentMasterList()
            {
                return _EstablishmentMasterRepository.EstablishmentMasterList();
            }

            public EstablishmentMasterModel EstablishmentRecord(int EstablishmentID)
            {
                var keyValuePairs = _EstablishmentMasterRepository.EstablishmentRecord(EstablishmentID);
                return keyValuePairs;
            }

            public EstablishmentMasterModel SaveEstablishmentRecord(EstablishmentMasterModel Objest)
            {
                var keyValuePairs = _EstablishmentMasterRepository.SaveEstablishmentRecord(Objest);
                return keyValuePairs;
            }


            public EstablishmentMasterModel DeleteEstablishmentRecord( int EstablishmentID)
            {
                var keyValuePairs = _EstablishmentMasterRepository.DeleteEstablishmentRecord(EstablishmentID);
                return keyValuePairs;
            }
        }   

        }

