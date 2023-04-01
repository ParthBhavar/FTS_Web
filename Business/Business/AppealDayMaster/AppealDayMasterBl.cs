using FTS.Data.ApplealDayMaster;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.AppealDayMaster
{
    public class AppealDayMasterBl : IAppealDayMasterBl
    {
        private readonly IAppealDayMasterRepository _AppeallDayRepository;

        public AppealDayMasterBl(IAppealDayMasterRepository AppealDayMasterRepositor)
        {
            this._AppeallDayRepository = AppealDayMasterRepositor;
        }
        public List<AppealDayMasterModel> AppealDayMasterList()
        {
            return _AppeallDayRepository.AppealDayMasterList();
        }

        public AppealDayMasterModel GetAppealDayRecord(int ID)
        {
            var keyValuePairs = _AppeallDayRepository.GetAppealDayRecord(ID);
            return keyValuePairs;
        }

        public AppealDayMasterModel SaveAppealDayRecord(AppealDayMasterModel ObjAppeal)
        {
            var keyValuePairs = _AppeallDayRepository.SaveAppealDayRecord(ObjAppeal);
            return keyValuePairs;
        }

        public AppealDayMasterModel DeleteAppealDayRecord(int UserID, int ID)
        {
            var keyValuePairs = _AppeallDayRepository.DeleteAppealDayRecord(UserID, ID);
            return keyValuePairs;
        }
    }
}
