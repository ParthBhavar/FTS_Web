using FTS.Data.DashBoard;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.DashBoard
{
    public class DashBoardBI : IDashBoardBI
    {
        private readonly IDashBoardRepository _DashBoardDetailRepository;

        public DashBoardBI(IDashBoardRepository DashBoardDetailRepository)
        {
            this._DashBoardDetailRepository = DashBoardDetailRepository;
        }
        public DashBoardModel DashBoardDetail(DashBoardModel Objdash)
        {
            var keyValuePairs = _DashBoardDetailRepository.DashBoardDetail(Objdash);
            return keyValuePairs;
        }

        public DashBoardModel CaseClimeDetail(DashBoardModel Objdash)
        {
            var keyValuePairs = _DashBoardDetailRepository.CaseClimeDetail(Objdash);
            return keyValuePairs;
        }
    }
}
