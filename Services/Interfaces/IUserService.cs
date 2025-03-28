namespace Contacts.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        List<User> FindAll();
        User GetByUsername(string username);

    }
}
