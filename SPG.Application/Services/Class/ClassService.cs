using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class ClassService(IClassRepository repository, IMapper mapper, IClassScheduleService classScheduleService) : IClassService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IClassRepository _repository = repository;
    private readonly IClassScheduleService _classScheduleService = classScheduleService;

    public IEnumerable<ClassDto> GetAllClasses()
    {
      var classes = _repository.GetAll().ToList();
      if (!classes.Any())
        return new List<ClassDto>();

      var classesDto = new List<ClassDto>();

      classes.ForEach(classObj => { classesDto.Add(_mapper.Map<ClassDto>(classObj)); });

      return classesDto;
    }

    public ClassDto GetClassById(int id)
    {
      return _mapper.Map<ClassDto>(_repository.GetById(id));
    }

    public ClassDto AddClass(ClassDto dto)
    {
      var classObj = _mapper.Map<ClassModel>(dto);
      classObj.Name = GenerateNameClass(dto);

      _repository.Add(classObj);

      return _mapper.Map<ClassDto>(classObj);
    }

    public ClassDto UpdateClass(ClassDto classObj)
    {
      _repository.Update(_mapper.Map<ClassModel>(classObj));

      return classObj;
    }

    public void DeleteClass(int id)
    {
      _classScheduleService.RemoveClassIdFromRelatedClasses(id);
      _repository.Delete(id);
    }

    private static string GenerateNameClass(ClassDto dto)
    {
      var date = DateTime.UtcNow;
      var semester = date.Month < 6 ? "1" : "2";
      var random = new Random(date.Day + date.Month + date.Year);
      int randomNumber = random.Next(100, int.MaxValue);

      return $"0{randomNumber}{dto.CurriculumId}_{semester}_{date.Year}";
    }
  }

}
