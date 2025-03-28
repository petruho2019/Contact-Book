using Contacts.Repository.Interfaces;
using Contacts.Services.Interfaces;

namespace Contacts.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public User Add(User entity)
        {
            return _repo.Add(entity);
        }

        public void Delete(User entity)
        {
            _repo.Delete(entity);
        }

        public List<User> FindAll()
        {
            return _repo.FindAll();
        }
        public User GetByUsername(string username)
        {
            return _repo.GetByUsername(username)!;
        }

        public int SaveChanges()
        {
            return _repo.SaveShanges();
        }
    }
}
