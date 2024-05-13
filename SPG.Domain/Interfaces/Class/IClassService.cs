using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface IClassService
    {
        IEnumerable<ClassDto> GetAllClasses();
        ClassDto GetClassById(int id);
        ClassDto AddClass(ClassDto curriculum);
        ClassDto UpdateClass(ClassDto curriculum);
        void DeleteClass(int id);
    }
}
