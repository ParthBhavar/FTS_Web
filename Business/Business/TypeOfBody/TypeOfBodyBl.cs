using FTS.Data.TypeOfBody;
using FTS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTS.Business.TypeOfBody
{
    public class TypeOfBodyBl : ITypeOfBodyBl
    {
        public readonly ITypeOfBodyRepository _TypeOfBodyRepository;

        public TypeOfBodyBl(ITypeOfBodyRepository TypeOfBodyRepositor)
        {
            this._TypeOfBodyRepository = TypeOfBodyRepositor;
        }

        public List<TypeOfBodyModel> TypeOfBodyList()
        {
            return _TypeOfBodyRepository.TypeOfBodyList();
        }

        public TypeOfBodyModel TypeOfBodyRecord(int TypeOfBodyID)
        {
            var keyValuePairs = _TypeOfBodyRepository.TypeOfBodyRecord(TypeOfBodyID);
            return keyValuePairs;
        }

        public TypeOfBodyModel SaveTypeOfBodyRecord(TypeOfBodyModel Objtypeofbody)
        {
            var keyValuePairs = _TypeOfBodyRepository.SaveTypeOfBodyRecord(Objtypeofbody);
            return keyValuePairs;
        }

        public TypeOfBodyModel DeleteTypeOfBody(int TypeOfBodyID, int UserID)
        {
            var keyValuePairs = _TypeOfBodyRepository.DeleteTypeOfBody(TypeOfBodyID, UserID);
            return keyValuePairs;
        }
    }
}
