using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class SystemParamsService(ISystemParamsRepository repository, IMapper mapper) : ISystemParamsService
  {
    private readonly IMapper _mapper = mapper;
    private readonly ISystemParamsRepository _repository = repository;

    public IEnumerable<SystemParamsDto> GetAllSystemParamss()
    {
      var systemParams = _repository.GetAll().ToList();
      if (!systemParams.Any())
        return new List<SystemParamsDto>();

      var systemParamsDto = new List<SystemParamsDto>();

      systemParams.ForEach(systemParam => { systemParamsDto.Add(_mapper.Map<SystemParamsDto>(systemParam)); });

      return systemParamsDto;
    }

    public SystemParamsDto GetSystemParamsById(string id)
    {
      return _mapper.Map<SystemParamsDto>(_repository.GetById(id));
    }

    public SystemParamsDto AddSystemParams(SystemParamsDto dto)
    {
      var systemParam = _mapper.Map<SystemParamsModel>(dto);

      _repository.Add(systemParam);

      return _mapper.Map<SystemParamsDto>(systemParam);
    }

    public SystemParamsDto UpdateSystemParams(SystemParamsDto systemParam)
    {
      _repository.Update(_mapper.Map<SystemParamsModel>(systemParam));

      return systemParam;
    }

    public void DeleteSystemParams(string id)
    {
      _repository.Delete(id);
    }
  }

}
