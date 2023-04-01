using FTS.Data.ZoneMaster;
using FTS.Model.Entities;
using Master.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.ZoneMaster
{
    public class ZoneMasterBl : IZoneMasterBl
    {
        private readonly IZoneMasterRepository _ZoneMasterRepository;

        public ZoneMasterBl(IZoneMasterRepository ZoneMasterRepositor)
        {
            this._ZoneMasterRepository = ZoneMasterRepositor;
        }

        public List<ZoneMasterModel> ZoneList()
        {
            return _ZoneMasterRepository.ZoneList();
        }

        public ZoneMasterModel ZoneRecord(int ZoneID)
        {
            var keyValuePairs = _ZoneMasterRepository.ZoneRecord(ZoneID);
            return keyValuePairs;
        }

        public ZoneMasterModel SaveZoneRecord(ZoneMasterModel ObjZone)
        {
            var keyValuePairs = _ZoneMasterRepository.SaveZoneRecord(ObjZone);
            return keyValuePairs;
        }

        public ZoneMasterModel DeleteZoneRecord(int UserID,int ZoneID)
        {
            var keyValuePairs = _ZoneMasterRepository.DeleteZoneRecord(UserID,ZoneID);
            return keyValuePairs;
        }

    }
}
