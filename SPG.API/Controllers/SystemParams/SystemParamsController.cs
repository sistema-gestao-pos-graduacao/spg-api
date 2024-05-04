using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPG.API.Controllers.Base;
using SPG.Domain.Dto;
using SPG.Domain.Interfaces;

namespace SPG.API.Controllers.SystemParams
{

  [Authorize(Roles = "Admin")]
  [Route("api/[controller]")]
  [ApiController]
  public class SystemParamsController(ISystemParamsService systemParamService) : SPGBaseController<SystemParamsDto>
  {
    private readonly ISystemParamsService _systemParamsService = systemParamService;

    [HttpGet]
    public ActionResult<IEnumerable<SystemParamsDto>> GetAll([FromQuery] Dictionary<string, string> filters)
    {
      var systemParams = _systemParamsService.GetAllSystemParamss();
      return Ok(ApplyFilters(systemParams, filters));
    }

    [HttpGet("{id}")]
    public ActionResult<SystemParamsDto> Get(string id)
    {
      var systemParam = _systemParamsService.GetSystemParamsById(id);
      if (systemParam == null)
      {
        return NotFound();
      }
      return Ok(systemParam);
    }

    [HttpPost]
    public ActionResult<SystemParamsDto> Create(SystemParamsDto systemParamDto)
    {
      try
      {
        var createdSystemParams = _systemParamsService.AddSystemParams(systemParamDto);
        return CreatedAtAction("Create", createdSystemParams);
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    [HttpPut]
    
    public IActionResult Update(SystemParamsDto systemParamDto)
    {
      _systemParamsService.UpdateSystemParams(systemParamDto);
      return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
      _systemParamsService.DeleteSystemParams(id);
      return NoContent();
    }

  }
}
