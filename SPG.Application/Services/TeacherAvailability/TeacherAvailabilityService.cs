using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

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

      teacherAvailabilities.ForEach(subject => { teacherAvailabilitiesDto.Add(_mapper.Map<TeacherAvailabilityDto>(subject)); });

      return teacherAvailabilitiesDto;
    }

    public TeacherAvailabilityDto GetTeacherAvailabilityById(int id)
    {
      return _mapper.Map<TeacherAvailabilityDto>(_repository.GetById(id));
    }

    public TeacherAvailabilityDto AddTeacherAvailability(TeacherAvailabilityDto dto)
    {
      var subject = _mapper.Map<TeacherAvailabilityModel>(dto);

      _repository.Add(subject);

      return _mapper.Map<TeacherAvailabilityDto>(subject);
    }

    public TeacherAvailabilityDto UpdateTeacherAvailability(TeacherAvailabilityDto subject)
    {
      _repository.Update(_mapper.Map<TeacherAvailabilityModel>(subject));

      return subject;
    }

    public void DeleteTeacherAvailability(int id)
    {
      _repository.Delete(id);
    }
  }

}
