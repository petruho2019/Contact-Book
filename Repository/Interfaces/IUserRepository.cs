namespace Contacts.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByUsername(string username);
        bool ExistByUsername(string username);
        Result<bool> Exist(User user);
        List<User> FindAll();
        void Delete(User user);
    }
}
