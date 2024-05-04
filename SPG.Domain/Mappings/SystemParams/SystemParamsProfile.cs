using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Model;

namespace SPG.Domain.Mappings
{
  public class SystemParamsProfile : Profile
  {
    public SystemParamsProfile()
    {
      CreateMap<SystemParamsModel, SystemParamsDto>()
        .ReverseMap();
    }
  }
}
