using SPG.Domain.Dto;
using SPG.Domain.Interfaces;
using System.Reflection;

namespace SPG.Domain.SystemParams
{
  public class SystemParams
  {
    private readonly ISystemParamsService? _service;

    public SystemParams(ISystemParamsService service)
    {
      _service = service;

      var systemParamsDtoList = _service.GetAllSystemParams();

      if (systemParamsDtoList != null && systemParamsDtoList.Any())
        MapSystemParams(systemParamsDtoList);
    }

    private void MapSystemParams(IEnumerable<SystemParamsDto> systemParamsDtoList)
    {
      var systemParamsProperties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

      foreach (var systemParamsDto in systemParamsDtoList)
      {
        var matchingProperty = systemParamsProperties.FirstOrDefault(p => p.Name == systemParamsDto.Id);

        if (matchingProperty == null)
          continue;

        if (matchingProperty.PropertyType == typeof(string))
          matchingProperty.SetValue(this, systemParamsDto.String);
        else if (matchingProperty.PropertyType == typeof(int))
          matchingProperty.SetValue(this, systemParamsDto.Integer);
        else if (matchingProperty.PropertyType == typeof(bool))
          matchingProperty.SetValue(this, systemParamsDto.Boolean);
        else if (matchingProperty.PropertyType == typeof(double) || matchingProperty.PropertyType == typeof(float))
          matchingProperty.SetValue(this, systemParamsDto.Double);
        else
          continue;
      }
    }

    public string SmtpPort { get; set; } = string.Empty;
    public string SmtpServerAddress { get; set; } = string.Empty;
    public string SmtpFromEmail { get; set; } = string.Empty;
    public string SmtpUsername { get; set; } = string.Empty;
    public string SmtpPassword { get; set; } = string.Empty;
  }
}