using Contacts.Contexts;
using Contacts.Repository.Implementations;
using Contacts.Repository.Interfaces;
using Contacts.Services.Interfaces;

namespace Contacts.Services.Implementations
{
    public class ContactBookService : IContactBookService
    {

        private readonly IContactBookRepository _ContactBookRepo;
        private readonly IUserRepository _UserRepo;
        private readonly IContactRepository _ContactRepo;
        public ContactBookService(IContactRepository contactRepo, IContactBookRepository ContactBookRepo, IUserRepository UserRepo) {
            _ContactBookRepo = ContactBookRepo;
            _UserRepo = UserRepo;
            _ContactRepo = contactRepo;
        }
        public Result<ContactBook> CreateContactBook(ContactBook contactBook, string username)
        {
            if (_ContactBookRepo.GetContactBookByName(contactBook.Name) != null)
            {
                return Result<ContactBook>.Failure(null, $"Contact book with name: \"{contactBook.Name}\" is busy");
            }
            contactBook.DateOfCreate = DateTime.UtcNow;
            contactBook.UserId = _UserRepo.GetByUsername(username)!.Id;

            _ContactBookRepo.Add(contactBook);
            _ContactBookRepo.SaveChanges();
            
            return Result<ContactBook>.Success(contactBook);
        }

        public void Delete(string name)
        {
            _ContactBookRepo.Delete(name);
            _ContactBookRepo.SaveShanges();
        }

        public List<ContactBook> GetContactBooksByUsername(string username)
        {
            return _ContactBookRepo.GetContactBooksByUsername(username);
        }

        public List<Contact> GetContactsByContactBookName(string name)
        {
            return _ContactRepo.GetContactsByContactBookName(name);
        }
    }
}
