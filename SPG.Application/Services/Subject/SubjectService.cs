﻿using AutoMapper;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Domain.Utils;

namespace SPG.Application.Services
{
  public class SubjectService(ISubjectRepository repository, IMapper mapper) : ISubjectService
  {
    private readonly IMapper _mapper = mapper;
    private readonly ISubjectRepository _repository = repository;

    public IEnumerable<SubjectDto> GetAllSubjects()
    {
      var subjects = _repository.GetAll().ToList();
      if (!subjects.Any())
        return new List<SubjectDto>();

      var subjectsDto = new List<SubjectDto>();

      subjects.ForEach(subject => { subjectsDto.Add(_mapper.Map<SubjectDto>(subject)); });

      return subjectsDto;
    }

    public SubjectDto GetSubjectById(int id)
    {
      return _mapper.Map<SubjectDto>(_repository.GetById(id));
    }

    public SubjectDto AddSubject(SubjectDto dto)
    {
      AddHexColor(dto);
      var subject = _mapper.Map<SubjectModel>(dto);

      _repository.Add(subject);

      return _mapper.Map<SubjectDto>(subject);
    }

    public IList<SubjectDto> AddSubjects(List<SubjectDto> dtoList)
    {
      foreach(var d in dtoList)
        AddHexColor(d);

      List<SubjectModel> subjects = _mapper.Map<List<SubjectModel>>(dtoList);

      if(subjects.Any())
        _repository.AddAll(subjects);

      return _mapper.Map<List<SubjectDto>>(subjects);
    }

    public SubjectDto UpdateSubject(SubjectDto subject)
    {
      _repository.Update(_mapper.Map<SubjectModel>(subject));

      return subject;
    }

    public void DeleteSubject(int id)
    {
      _repository.Delete(id);
    }
    public void DeleteSubjects(List<int> ids)
    {
      _repository.DeleteAll(ids);
    }

    private static void AddHexColor(SubjectDto subject)
    {
      subject.Color = ColorUtils.GenerateHexColor(subject.Name);
    }
  }

}
