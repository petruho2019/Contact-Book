using Contacts.Contexts;
using Contacts.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Repository.Implementations
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationContext _context;
        public ContactRepository(ApplicationContext context) => _context = context;

        public Contact Add(Contact entity)
        {
            return _context.Contacts.Add(entity).Entity;
        }

        public void Delete(Contact contact)
        {
            _context.Contacts.Remove(contact);
        }

        public Contact? GetById(int id)
        {
            return _context.Contacts.Where(c => c.Id == id).First();
        }

        public List<Contact> GetContactsByContactBookName(string name)
        {
            return [.. _context.Contacts.Where(c => c.ContactBookId == c.ContactBook.Id && c.ContactBook.Name == name)];
        }

        public List<Contact> GetContactsByUsername(string username)
        {
            return [.. _context.Contacts.Where(c => c.ContactBookId == c.ContactBook.Id && c.ContactBook.User.Username == username)];
        }

        public int SaveShanges()
        {
            return _context.SaveChanges();
        }
    }
}
