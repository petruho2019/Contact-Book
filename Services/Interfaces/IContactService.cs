namespace Contacts.Services.Interfaces
{
    public interface IContactService 
    {
        List<Contact> GetContactsByUsername(string username);

        List<Contact> GetContactsByContactBookName(string name);
        Contact Add(Contact contact, string contactBookName);

        void Delete(Contact contact);
        Contact Edit(Contact newContact);
    }
}
