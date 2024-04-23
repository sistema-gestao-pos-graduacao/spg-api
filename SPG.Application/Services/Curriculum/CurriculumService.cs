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
      var curriculums = _repository.GetAll().ToList();
      if (!curriculums.Any())
        return new List<CurriculumDto>();

      var curriculumsDto = new List<CurriculumDto>();

      curriculums.ForEach(curriculum => { curriculumsDto.Add(_mapper.Map<CurriculumDto>(curriculum)); });

      return curriculumsDto;
    }

    public CurriculumDto GetCurriculumById(int id)
    {
      return _mapper.Map<CurriculumDto>(_repository.GetById(id));
    }

    public CurriculumDto AddCurriculum(CurriculumDto dto)
    {
      var curriculum = _mapper.Map<CurriculumModel>(dto);

      _repository.Add(curriculum);

      return _mapper.Map<CurriculumDto>(curriculum);
    }

    public CurriculumDto UpdateCurriculum(CurriculumDto curriculum)
    {
      _repository.Update(_mapper.Map<CurriculumModel>(curriculum));

      return curriculum;
    }

    public void DeleteCurriculum(int id)
    {
      _repository.Delete(id);
    }
  }

}
