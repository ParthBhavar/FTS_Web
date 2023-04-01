using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.DashBoard
{
    public interface IDashBoardRepository
    {
        DashBoardModel DashBoardDetail(DashBoardModel DashBoardRepo);
        DashBoardModel CaseClimeDetail(DashBoardModel DashBoardRepo);
    }
}
