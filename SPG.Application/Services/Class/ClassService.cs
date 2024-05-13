using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class ClassService(IClassRepository repository, IMapper mapper) : IClassService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IClassRepository _repository = repository;

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
      _repository.Delete(id);
    }
  }

}
