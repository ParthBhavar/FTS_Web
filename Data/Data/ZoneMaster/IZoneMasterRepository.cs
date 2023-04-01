using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.ZoneMaster
{
    public interface IZoneMasterRepository
    {

        List<ZoneMasterModel> ZoneList();
        ZoneMasterModel ZoneRecord(int ZoneID);
        ZoneMasterModel SaveZoneRecord(ZoneMasterModel ObjZone);
        ZoneMasterModel DeleteZoneRecord(int UserID, int ZoneID);
    }
}
