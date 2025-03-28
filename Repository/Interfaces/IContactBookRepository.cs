namespace Contacts.Repository.Interfaces
{
    public interface IContactBookRepository : IRepository<ContactBook>
    {
        ContactBook? GetContactBookByName(string name);
        void SaveChanges();
        List<ContactBook> GetContactBooksByUsername(string username);

        void Delete(string name);
    }
}
