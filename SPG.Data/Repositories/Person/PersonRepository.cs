using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;
using Microsoft.EntityFrameworkCore;

namespace SPG.Data.Repositories
{
  public class PersonRepository(AppDbContext context, IUserService userService) : IPersonRepository
  {
    private readonly AppDbContext _context = context;
    private readonly IUserService _userService = userService;
    public IEnumerable<PersonModel> GetAll()
    {
      return _context.Persons.Include(c => c.User).ToList();
    }

    public PersonModel GetById(int id)
    {
      var person = _context.Persons.Include(c => c.User).FirstOrDefault(p => p.Id == id);

      return person ?? throw new Exception(string.Format(Resources.NotFoundPerson, id));
    }
    
    public PersonModel GetByUserId(string userId)
    {
      var person = _context.Persons.Include(c => c.User).FirstOrDefault(p => p.UserId == userId);

      return person ?? throw new Exception(string.Format(Resources.NotFoundPerson, userId));
    }

    public void Add(PersonModel person)
    {
      _context.Persons.Add(person);
      _context.SaveChanges();
      person.Id = _context.Persons.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
    }

    public void Update(PersonModel person)
    {
      try
      {
        var model = GetById(person.Id);
        model.Name = person.Name;
        model.Cpf = person.Cpf; 
        model.BirthDate = person.BirthDate; 
        model.UserId = person.UserId; 
        model.Email = person.Email;

        _context.Entry(model).State = EntityState.Modified;
        _context.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }

    public async Task Delete(int id)
    {
      var person = _context.Persons.FirstOrDefault(p => p.Id == id);
      if (person != null)
      {
        try
        {
          _context.Persons.Remove(person);
          await _userService.DeleteUserAsync(person.UserId);
          _context.SaveChanges();
        }
        catch (Exception ex)
        {
          throw new Exception(ex.Message);
        }
      }
    }
  }
}
