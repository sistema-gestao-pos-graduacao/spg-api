using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;

namespace SPG.Application.Services
{
  public class CurriculumService(ICurriculumRepository repository, IMapper mapper) : ICurriculumService
  {
    private readonly IMapper _mapper = mapper;
    private readonly ICurriculumRepository _repository = repository;

    public IEnumerable<CurriculumDto> GetAllCurriculums()
    {
      var subjects = _repository.GetAll().ToList();
      if (!subjects.Any())
        return new List<CurriculumDto>();

      var subjectsDto = new List<CurriculumDto>();

      subjects.ForEach(subject => { subjectsDto.Add(_mapper.Map<CurriculumDto>(subject)); });

      return subjectsDto;
    }

    public CurriculumDto GetCurriculumById(int id)
    {
      return _mapper.Map<CurriculumDto>(_repository.GetById(id));
    }

    public CurriculumDto AddCurriculum(CurriculumDto dto)
    {
      var subject = _mapper.Map<CurriculumModel>(dto);

      _repository.Add(subject);

      return _mapper.Map<CurriculumDto>(subject);
    }

    public CurriculumDto UpdateCurriculum(CurriculumDto subject)
    {
      _repository.Update(_mapper.Map<CurriculumModel>(subject));

      return subject;
    }

    public void DeleteCurriculum(int id)
    {
      _repository.Delete(id);
    }
  }

}
