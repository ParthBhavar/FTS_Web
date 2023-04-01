using FTS.Data.CessProjectDetails;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.CessProjectDetails
{
    public class ProjectDetailsBl : IProjectDetailsBl
    {
        private readonly ICessProjectDetailsRepository _ProjectDetailsRepository;

        public ProjectDetailsBl(ICessProjectDetailsRepository ProjectDetailsRepositor)
        {
            this._ProjectDetailsRepository = ProjectDetailsRepositor;
        }
        public List<CessProjectDetailsModel> ProjectDetailsList()
        {
            return _ProjectDetailsRepository.ProjectDetailsList();
        }
        public CessProjectDetailsModel ProjectDetailsRecord(int ProjectID)
        {
            var keyValuePairs = _ProjectDetailsRepository.ProjectDetailsRecord(ProjectID);
            return keyValuePairs;
        }
        public CessProjectDetailsModel SaveProjectDetails(CessProjectDetailsModel ObjProjectdtl)
        {
            var keyValuePairs = _ProjectDetailsRepository.SaveProjectDetails(ObjProjectdtl);
            return keyValuePairs;
        }

        public CessProjectDetailsModel DeleteProjectDetails(int UserID, int ProjectID)
        {
            var keyValuePairs = _ProjectDetailsRepository.DeleteProjectDetails(UserID, ProjectID);
            return keyValuePairs;
        }

        public CessProjectDetailsModel CessCollectionDetailsRecord(int EstablishmentID)
        {
            var keyValuePairs = _ProjectDetailsRepository.CessCollectionDetailsRecord(EstablishmentID);
            return keyValuePairs;
        }
        public CessProjectDetailsModel SaveCessCollectionDetails(CessProjectDetailsModel ObjCessCollectdtl)
        {
            var keyValuePairs = _ProjectDetailsRepository.SaveCessCollectionDetails(ObjCessCollectdtl);
            return keyValuePairs;
        }
        public List<CessProjectDetailsModel> CessCollectionList(CessProjectDetailsModel ObjCessCollectdtl)
        {
            return _ProjectDetailsRepository.CessCollectionList(ObjCessCollectdtl);
        }

    }
}
