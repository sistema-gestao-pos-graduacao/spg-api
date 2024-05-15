using AutoMapper;
using SPG.Application.Properties;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Domain.Utils;

namespace SPG.Application.Services
{
  public class ClassScheduleService(IClassScheduleRepository repository, IClassRepository classRepository, IMapper mapper) : IClassScheduleService
  {
    private readonly IMapper _mapper = mapper;
    private readonly IClassScheduleRepository _repository = repository;
    private readonly IClassRepository _classRepository = classRepository;

    public IEnumerable<ClassScheduleDto> GetAllClassSchedules()
    {
      var classSchedules = _repository.GetAll().ToList();
      if (!classSchedules.Any())
        return new List<ClassScheduleDto>();

      return _mapper.Map<List<ClassScheduleDto>>(classSchedules);
    }

    public IEnumerable<ClassScheduleDto> GetAllFilteredByClass(int id)
    {
      var classSchedules = _repository.GetAll().ToList();
      if (!classSchedules.Any())
        return new List<ClassScheduleDto>();

      return _mapper.Map<List<ClassScheduleDto>>(classSchedules.Where(item => id == item.Id)).ToList();
    }

    public ClassScheduleDto GetClassScheduleById(int id)
    {
      return _mapper.Map<ClassScheduleDto>(_repository.GetById(id));
    }

    public ClassScheduleDto AddClassSchedule(ClassScheduleDto dto)
    {
      VerifyRelatedClasses(dto);
      AddHexColor(dto);
      var classSchedule = _mapper.Map<ClassScheduleModel>(dto);

      _repository.Add(classSchedule);

      return _mapper.Map<ClassScheduleDto>(classSchedule);
    }
    public IList<ClassScheduleDto> AddClassSchedules(List<ClassScheduleDto> dtoList)
    {
      foreach (var d in dtoList)
      {
        VerifyRelatedClasses(d);
        AddHexColor(d);
      }

      List<ClassScheduleModel> classSchedules = _mapper.Map<List<ClassScheduleModel>>(dtoList);

      if (classSchedules.Any())
        _repository.AddAll(classSchedules);

      return _mapper.Map<List<ClassScheduleDto>>(classSchedules);
    }

    public ClassScheduleDto UpdateClassSchedule(ClassScheduleDto classSchedule)
    {
      VerifyRelatedClasses(classSchedule);
      _repository.Update(_mapper.Map<ClassScheduleModel>(classSchedule));

      return classSchedule;
    }

    public void DeleteClassSchedule(int id)
    {
      _repository.Delete(id);
    }

    public void DeleteClassSchedules(List<int> ids)
    {
      _repository.DeleteAll(ids);
    }

    private static void AddHexColor(ClassScheduleDto classSchedule)
    {
      classSchedule.Color = ColorUtils.GenerateHexColor(classSchedule.StartDateTime.ToString());
    }

    private void VerifyRelatedClasses(ClassScheduleDto classSchedule)
    {
      var duplicates = classSchedule.RelatedClassesIds
        .GroupBy(x => x)
        .Where(group => group.Count() > 1)
        .Select(group => group.Key);

      if (duplicates.Any())
        throw new Exception(Resources.RelatedClassesRepeationValidation + string.Join(", ", duplicates));

      var classesIds = _classRepository.GetAll().Select(c => c.Id).ToList();

      List<int> invalidIds = [];

      foreach (var item in classSchedule.RelatedClassesIds)
      {
        if (!classesIds.Contains(item))
          invalidIds.Add(item);
      }

      if(invalidIds.Any())
        throw new Exception(Resources.InvalidClassesIdsValidation + string.Join(", ", invalidIds));
    }

    public void RemoveClassIdFromRelatedClasses(int classId)
    {
      try
      {
        var classSchedules = _repository.GetAllClassesScheduleByRelatedClassId(classId);

        foreach (var item in classSchedules)
        {
          item.RelatedClassesIds.Remove(classId);
          _repository.Update(item);
        }
      }
      catch (Exception)
      {
        throw;
      }
    }
  }

}
