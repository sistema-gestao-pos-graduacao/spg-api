using SPG.Domain.Model;

namespace SPG.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAll();
        UserModel GetById(int id);
        void Add(UserModel person);
        void Update(UserModel person);
        void Delete(int id);
    }
}
