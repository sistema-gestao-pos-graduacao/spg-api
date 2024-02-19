using SPG.Data.Context;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

namespace SPG.Data.User
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserModel GetById(int id)
        {
            var person = _context.Users.FirstOrDefault(p => p.Id == id);

            return person ?? throw new Exception(string.Format(Resources.NotFoundUser, id));
        }

        public void Add(UserModel person)
        {
            _context.Users.Add(person);
            person.Id = _context.SaveChanges();
        }

        public void Update(UserModel person)
        {
            try
            {
                _context.Users.Update(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var person = _context.Users.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _context.Users.Remove(person);
                _context.SaveChanges();
            }
        }
    }
}
