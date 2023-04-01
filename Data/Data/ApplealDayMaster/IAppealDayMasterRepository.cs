using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ApplealDayMaster
{
    public interface IAppealDayMasterRepository
    {
        List<AppealDayMasterModel> AppealDayMasterList();
        AppealDayMasterModel GetAppealDayRecord(int ID);
        AppealDayMasterModel SaveAppealDayRecord(AppealDayMasterModel ObjAppeal);
        AppealDayMasterModel DeleteAppealDayRecord(int UserID, int ID);
    }
}
