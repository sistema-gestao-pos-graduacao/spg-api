using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<CourseModel> GetAll();
        CourseModel GetById(int id);
        void Add(CourseModel course);
        void Update(CourseModel course);
        void Delete(int id);
    }
}
