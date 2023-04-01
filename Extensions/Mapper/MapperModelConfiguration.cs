using AutoMapper;
using FTS.Model.DTO;
using FTS.Model.Entities;
using System;

namespace Mapper
{
    public class MapperModelConfiguration : Profile
    {
        public MapperModelConfiguration()
        {
            CreateMap<LoginAuditDTO, LoginAudit>();
            CreateMap<LoginAudit, LoginAuditDTO>();
           
        }
    }
}
