using SPG.Data.Context;
using SPG.Domain.Interfaces.Person;
using SPG.Domain.Model.Person;

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

            return person ?? throw new Exception($"Não existe pessoa cadastrada com o id:{id}");
        }

        public void Add(PersonModel person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void Update(PersonModel person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
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
