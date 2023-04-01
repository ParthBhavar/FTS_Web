using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.CessProjectDetails
{
    public interface ICessProjectDetailsRepository
    {
        List<CessProjectDetailsModel> ProjectDetailsList();
        CessProjectDetailsModel ProjectDetailsRecord(int ProjectID);
        CessProjectDetailsModel SaveProjectDetails(CessProjectDetailsModel ObjProjectdtl);
        CessProjectDetailsModel DeleteProjectDetails(int UserID, int ProjectID);
        CessProjectDetailsModel CessCollectionDetailsRecord(int EstablishmentID);
        CessProjectDetailsModel SaveCessCollectionDetails(CessProjectDetailsModel ObjCessCollectdtl);
        List<CessProjectDetailsModel> CessCollectionList(CessProjectDetailsModel ObjCessCollectdtl);
    }
}
