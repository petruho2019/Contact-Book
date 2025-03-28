using BCrypt.Net;
using Contacts.Contexts;
using Contacts.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Contacts.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) => _context = context;

        public Result<bool> Exist(User user)
        {
            if (!ExistByUsername(user.Username))
            {
                if (user.Username == "")
                {
                    return Result<bool>.Failure("Username", $"User with username: \" {user.Username} \" not found");
                }
                return Result<bool>.Failure("Username", $"User with username: \" {user.Username} \" not found");
            }

            User userFromDb = _context.Users.FirstOrDefault(u => u.Username == user.Username)!;

            if (!BCrypt.Net.BCrypt.Verify(user.Password, userFromDb.Password))
            {
                return Result<bool>.Failure("Password"!, $"Incorrect password");
            }

            return Result<bool>.Success(true);
        }

        public bool ExistByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username) != null;
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User Add(User user)
        {
            return _context.Users.Add(user).Entity;
        }

        public List<User> FindAll()
        {
            return _context.Users.ToList();
        }

        public int SaveShanges()
        {
            return _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public User? GetById(int id)
        {
            return _context.Users.Where(c => c.Id == id).First();
        }
    }
}
