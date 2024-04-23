using SPG.Domain.Dto;

namespace SPG.Domain.Interfaces
{
    public interface ICurriculumService
    {
        IEnumerable<CurriculumDto> GetAllCurriculums();
        CurriculumDto GetCurriculumById(int id);
        CurriculumDto AddCurriculum(CurriculumDto curriculum);
        CurriculumDto UpdateCurriculum(CurriculumDto curriculum);
        void DeleteCurriculum(int id);
    }
}
