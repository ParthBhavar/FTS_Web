using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Data.TypeOfBody
{
    public interface ITypeOfBodyRepository
    {
        List<TypeOfBodyModel> TypeOfBodyList();
        TypeOfBodyModel TypeOfBodyRecord(int TypeOfBodyID);
        TypeOfBodyModel SaveTypeOfBodyRecord(TypeOfBodyModel Objtypeofbody);
        TypeOfBodyModel DeleteTypeOfBody(int TypeOfBodyID, int UserID);

    }
}
