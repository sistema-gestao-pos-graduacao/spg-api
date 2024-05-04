﻿using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ISystemParamsService
  {
        IEnumerable<SystemParamsDto> GetAllSystemParamss();
        SystemParamsDto GetSystemParamsById(string id);
        SystemParamsDto AddSystemParams(SystemParamsDto curriculum);
        SystemParamsDto UpdateSystemParams(SystemParamsDto curriculum);
        void DeleteSystemParams(string id);
    }
}
