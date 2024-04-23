using AutoMapper;
using SPG.Application.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Enums;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class CourseService(ICourseRepository repository, IMapper mapper, IPersonService personService) : ICourseService
  {
    private readonly IMapper _mapper = mapper;
    private readonly ICourseRepository _repository = repository;
    private readonly IPersonService _personService = personService;

    public IEnumerable<CourseDto> GetAllCourses()
    {
      var courses = _repository.GetAll().ToList();
      if (!courses.Any())
        return new List<CourseDto>();

      var coursesDto = new List<CourseDto>();

      courses.ForEach(course => { coursesDto.Add(_mapper.Map<CourseDto>(course)); });

      return coursesDto;
    }

    public CourseDto GetCourseById(int id)
    {
      return _mapper.Map<CourseDto>(_repository.GetById(id));
    }

    public CourseDto AddCourse(CourseDto dto)
    {
      if (dto.CoordinatorId.HasValue)
        ValidateCoordinatorPersonType(dto.CoordinatorId.Value);

      var course = _mapper.Map<CourseModel>(dto);

      _repository.Add(course);

      return _mapper.Map<CourseDto>(course);
    }

    public CourseDto UpdateCourse(CourseDto course)
    {
      if (course.CoordinatorId.HasValue)
        ValidateCoordinatorPersonType(course.CoordinatorId.Value);

      _repository.Update(_mapper.Map<CourseModel>(course));

      return course;
    }

    public void DeleteCourse(int id)
    {
      _repository.Delete(id);
    }

    private void ValidateCoordinatorPersonType(int coordinatorId)
    {
      var person = _personService.GetPersonById(coordinatorId);

      if (person.PersonType != PersonTypeEnum.Coordinator)
        throw new Exception(Resources.ValidCoordinatorCourse);
    }
  }

}
