using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourses();
        CourseDto GetCourseById(int id);
        CourseDto AddCourse(CourseDto course);
        CourseDto UpdateCourse(CourseDto course);
        void DeleteCourse(int id);
    }
}
