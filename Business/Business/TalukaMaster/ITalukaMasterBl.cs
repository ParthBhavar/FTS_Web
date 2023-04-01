using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TalukaMaster
{
    public interface ITalukaMasterBl
    {
        List<TalukaMasterModel> TalukaList();
        TalukaMasterModel TalukaRecord(int TalukaId);
        TalukaMasterModel SaveTalukaRecord(TalukaMasterModel ObjTaluka);
        TalukaMasterModel DeleteTalukaRecord(int UserID, int TalukaId);
    }
}
