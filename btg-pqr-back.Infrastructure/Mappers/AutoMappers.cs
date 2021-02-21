using AutoMapper;
using btg_pqr_back.Core.Commands;
using btg_pqr_back.Core.Entities;

namespace btg_pqr_back.Infrastructure.Mappers
{
    public class AutoMappers : Profile
    {
        public AutoMappers()
        {
            CreateMap<CreatePqrCommand, PqrEntity>().ReverseMap();
        }
    }
}
