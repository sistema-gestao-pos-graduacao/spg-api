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
      var user = _context.Users.FirstOrDefault(p => p.Id == id);

      return user ?? throw new Exception(string.Format(Resources.NotFoundUser, id));
    }

    public UserModel GetByLogin(string login)
    {
      var user = _context.Users.FirstOrDefault(p => p.Login == login);

      return user ?? throw new Exception(string.Format(Resources.NotFoundUser, login));
    }

    public void Add(UserModel user)
    {
      _context.Users.Add(user);
      user.Id = _context.SaveChanges();
    }

    public void Update(UserModel user)
    {
      try
      {
        _context.Users.Update(user);
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public void Delete(int id)
    {
      var user = _context.Users.FirstOrDefault(p => p.Id == id);
      if (user != null)
      {
        _context.Users.Remove(user);
        _context.SaveChanges();
      }
    }
  }
}
