namespace Contacts.Services.Interfaces
{
    public interface IContactBookService
    {
        Result<ContactBook> CreateContactBook(ContactBook contactBook, string username);

        List<ContactBook> GetContactBooksByUsername(string username);
        List<Contact> GetContactsByContactBookName(string name);
        void Delete(string name);
    }
}
