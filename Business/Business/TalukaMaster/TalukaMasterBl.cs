using FTS.Data.TalukaMater;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TalukaMaster
{
    public class TalukaMasterBl : ITalukaMasterBl
    {
        private readonly ITalukaMasterRepository _TalukaMasterRepository;

        public TalukaMasterBl(ITalukaMasterRepository TalukaMasterRepositor)
        {
            this._TalukaMasterRepository = TalukaMasterRepositor;
        }

       
        public List<TalukaMasterModel> TalukaList()
        {
            return _TalukaMasterRepository.TalukaList();
        }

        public TalukaMasterModel TalukaRecord(int TalukaId)
        {
            var keyValuePairs = _TalukaMasterRepository.TalukaRecord(TalukaId);
            return keyValuePairs;
        }

        public TalukaMasterModel SaveTalukaRecord(TalukaMasterModel ObjTaluka)
        {
            var keyValuePairs = _TalukaMasterRepository.SaveTalukaRecord(ObjTaluka);
            return keyValuePairs;
        }

        public TalukaMasterModel DeleteTalukaRecord(int UserID, int TalukaId)
        {
            var keyValuePairs = _TalukaMasterRepository.DeleteTalukaRecord(UserID, TalukaId);
            return keyValuePairs;
        }


    }
}
