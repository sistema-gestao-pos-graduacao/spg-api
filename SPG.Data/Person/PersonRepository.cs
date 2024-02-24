using SPG.Data.Context;
using SPG.Domain.Interfaces;
using SPG.Domain.Model;
using SPG.Data.Properties;

namespace SPG.Data.Person
{
    public class PersonRepository(AppDbContext context) : IPersonRepository
    {
        private readonly AppDbContext _context = context;

        public IEnumerable<PersonModel> GetAll()
        {
            return _context.Persons.ToList();
        }

        public PersonModel GetById(int id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);

            return person ?? throw new Exception(string.Format(Resources.NotFoundPerson, id));
        }

        public void Add(PersonModel person)
        {
            _context.Persons.Add(person);
            person.Id = _context.SaveChanges();
        }

        public void Update(PersonModel person)
        {
            try
            {
                _context.Persons.Update(person);
                _context.SaveChanges();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var person = _context.Persons.FirstOrDefault(p => p.Id == id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
    }
}
