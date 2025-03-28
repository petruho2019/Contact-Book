using Contacts.Contexts;
using Contacts.Models;
using Contacts.Repository.Interfaces;

namespace Contacts.Repository.Implementations
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly ApplicationContext _context;
        public ContactBookRepository(ApplicationContext context) => _context = context;

        public void Delete(string name)
        {
            _context.ContactBooks.Remove(_context.ContactBooks.Where(c => c.Name == name).First());
        }

        public ContactBook? GetById(int id)
        {
            return _context.ContactBooks.Where(c => c.Id == id).First();
        }

        public ContactBook? GetContactBookByName(string name)
        {
            return _context.ContactBooks.FirstOrDefault(c => c.Name == name);
        }

        public List<ContactBook> GetContactBooksByUsername(string username)
        {
            return [.._context.ContactBooks.Where(c => c.UserId == c.User.Id && c.User.Username == username)] ;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public int SaveShanges()
        {
            return _context.SaveChanges();
        }

        ContactBook IRepository<ContactBook>.Add(ContactBook entity)
        {
            return _context.ContactBooks.Add(entity).Entity; ;
        }
    }
}
