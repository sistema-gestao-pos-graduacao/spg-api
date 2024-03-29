﻿using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ITeacherAvailabilityRepository
  {
        IEnumerable<TeacherAvailabilityModel> GetAll();
        TeacherAvailabilityModel GetById(int id);
        void Add(TeacherAvailabilityModel person);
        void Update(TeacherAvailabilityModel person);
        void Delete(int id);
    }
}
