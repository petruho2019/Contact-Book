using Contacts.Repository.Interfaces;
using Contacts.Services.Interfaces;

namespace Contacts.Services.Implementations
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;
        private readonly IContactBookRepository _ContactBookRepo;

        public ContactService(IContactRepository repo, IContactBookRepository ContactBookRepo) {
            _repo = repo;
            _ContactBookRepo = ContactBookRepo;
        }

        public Contact Add(Contact contact, string contactBookName)
        {
            contact.ContactBookId = _ContactBookRepo.GetContactBookByName(contactBookName)!.Id;
            contact.DateOfCreate = DateTime.UtcNow;
            _repo.Add(contact);
            _repo.SaveShanges();
            return contact;
        }

        public void Delete(Contact contact)
        {
            _repo.Delete(contact);
            _repo.SaveShanges();
        }

        public Contact Edit(Contact newContact)
        {
            var contactFromDb = _repo.GetById(newContact.Id)!;
            contactFromDb.Name = newContact.Name;
            contactFromDb.Description = newContact.Description;
            _repo.SaveShanges();
            return contactFromDb;
        }

        public List<Contact> GetContactsByContactBookName(string name)
        {
            return _repo.GetContactsByContactBookName(name);
        }

        public List<Contact> GetContactsByUsername(string username)
        {
            return _repo.GetContactsByUsername(username);
        }
    }
}
