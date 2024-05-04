using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Domain.Utils;

namespace SPG.Application.Services
{
  public class ClassScheduleService(IClassScheduleRepository repository, IMapper mapper) : IClassScheduleService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IClassScheduleRepository _repository = repository;

    public IEnumerable<ClassScheduleDto> GetAllClassSchedules()
    {
      var classSchedules = _repository.GetAll().ToList();
      if (!classSchedules.Any())
        return new List<ClassScheduleDto>();

      var classSchedulesDto = new List<ClassScheduleDto>();

      classSchedules.ForEach(classSchedule => { classSchedulesDto.Add(_mapper.Map<ClassScheduleDto>(classSchedule)); });

      return classSchedulesDto;
    }

    public ClassScheduleDto GetClassScheduleById(int id)
    {
      return _mapper.Map<ClassScheduleDto>(_repository.GetById(id));
    }

    public ClassScheduleDto AddClassSchedule(ClassScheduleDto dto)
    {
      AddHexColor(dto);
      var classSchedule = _mapper.Map<ClassScheduleModel>(dto);

      _repository.Add(classSchedule);

      return _mapper.Map<ClassScheduleDto>(classSchedule);
    }
    public IList<ClassScheduleDto> AddClassSchedules(List<ClassScheduleDto> dtoList)
    {
      foreach (var d in dtoList)
        AddHexColor(d);

      List<ClassScheduleModel> classSchedules = _mapper.Map<List<ClassScheduleModel>>(dtoList);

      if (classSchedules.Any())
        _repository.AddAll(classSchedules);

      return _mapper.Map<List<ClassScheduleDto>>(classSchedules);
    }

    public ClassScheduleDto UpdateClassSchedule(ClassScheduleDto classSchedule)
    {
      _repository.Update(_mapper.Map<ClassScheduleModel>(classSchedule));

      return classSchedule;
    }

    public void DeleteClassSchedule(int id)
    {
      _repository.Delete(id);
    }

    private static void AddHexColor(ClassScheduleDto classSchedule)
    {
      classSchedule.Color = ColorUtils.GenerateHexColor(classSchedule.StartDateTime.ToString());
    }
  }

}
