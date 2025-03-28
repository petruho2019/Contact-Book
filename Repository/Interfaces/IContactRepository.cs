namespace Contacts.Repository.Interfaces
{
    public interface IContactRepository : IRepository<Contact>
    {
        List<Contact> GetContactsByUsername(string username);
        List<Contact> GetContactsByContactBookName(string name);

        void Delete(Contact contact);
    }
}
