using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Domain.Utils;

namespace SPG.Application.Services
{
  public class TeacherAvailabilityService(ITeacherAvailabilityRepository repository, IMapper mapper) : ITeacherAvailabilityService
  {
    private readonly IMapper _mapper = mapper;
    private readonly ITeacherAvailabilityRepository _repository = repository;

    public IEnumerable<TeacherAvailabilityDto> GetAllTeacherAvailabilities()
    {
      var teacherAvailabilities = _repository.GetAll().ToList();
      if (!teacherAvailabilities.Any())
        return new List<TeacherAvailabilityDto>();

      var teacherAvailabilitiesDto = new List<TeacherAvailabilityDto>();

      teacherAvailabilities.ForEach(teacherAvailability => { teacherAvailabilitiesDto.Add(_mapper.Map<TeacherAvailabilityDto>(teacherAvailability)); });

      return teacherAvailabilitiesDto;
    }

    public TeacherAvailabilityDto GetTeacherAvailabilityById(int id)
    {
      return _mapper.Map<TeacherAvailabilityDto>(_repository.GetById(id));
    }

    public TeacherAvailabilityDto AddTeacherAvailability(TeacherAvailabilityDto dto)
    {
      AddHexColor(dto);
      var teacherAvailability = _mapper.Map<TeacherAvailabilityModel>(dto);

      _repository.Add(teacherAvailability);

      return _mapper.Map<TeacherAvailabilityDto>(teacherAvailability);
    }
    public IList<TeacherAvailabilityDto> AddTeacherAvailabilities(List<TeacherAvailabilityDto> dtoList)
    {
      foreach (var d in dtoList)
        AddHexColor(d);

      List<TeacherAvailabilityModel> teacherAvailabilities = _mapper.Map<List<TeacherAvailabilityModel>>(dtoList);

      if (teacherAvailabilities.Any())
        _repository.AddAll(teacherAvailabilities);

      return _mapper.Map<List<TeacherAvailabilityDto>>(teacherAvailabilities);
    }

    public TeacherAvailabilityDto UpdateTeacherAvailability(TeacherAvailabilityDto teacherAvailability)
    {
      _repository.Update(_mapper.Map<TeacherAvailabilityModel>(teacherAvailability));

      return teacherAvailability;
    }

    public void DeleteTeacherAvailability(int id)
    {
      _repository.Delete(id);
    }

    public void DeleteTeacherAvailabilities(List<int> ids)
    {
      _repository.DeleteAll(ids);
    }

    private static void AddHexColor(TeacherAvailabilityDto teacherAvailability)
    {
      teacherAvailability.Color = ColorUtils.GenerateHexColor(teacherAvailability.StartDateTime.ToString());
    }
  }

}
