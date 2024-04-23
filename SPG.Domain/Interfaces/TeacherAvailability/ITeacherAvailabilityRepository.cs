﻿using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ITeacherAvailabilityRepository
  {
        IEnumerable<TeacherAvailabilityModel> GetAll();
        TeacherAvailabilityModel GetById(int id);
        void Add(TeacherAvailabilityModel teacherAvailability);
        void Update(TeacherAvailabilityModel teacherAvailability);
        void Delete(int id);
    }
}
