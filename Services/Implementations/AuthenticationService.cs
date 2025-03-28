using Contacts.Services.Interfaces;
using Contacts.Models;
using System.Security.Claims;
using Contacts.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contacts.Repository.Implementations;
using Contacts.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using IAuthenticationService = Contacts.Services.Interfaces.IAuthenticationService;

namespace Contacts.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IUserRepository _repo;

        public AuthenticationService(IUserRepository repo) => _repo = repo;

        public Result<List<Claim>> Registration(User user)
        {
            if (_repo.GetByUsername(user.Username) != null)
            {
                return Result<List<Claim>>.Failure("Username", $"Username: \"{user.Username}\" is busy")!;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            AddUserToDb(user);
            _repo.SaveShanges();

            var claims = new List<Claim> 
            { 
                new(ClaimsIdentity.DefaultNameClaimType, user.Username)
            };

            return Result<List<Claim>>.Success(claims)!;
        }
        public void SignOut(HttpContext context)
        {
            context.SignOutAsync();
            context.Response.Cookies.Delete(".AspNetCore.Application.Id");

        }

        public Result<List<Claim>> SignIn(User user)
        {
            var result = _repo.Exist(user);

            if (!result.IsSuccess)
            {
                return Result<List<Claim>>.Failure(result.ErrorField!, result.ErrorMessage!)!;
            }

            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Username)
            };

            return Result<List<Claim>>.Success(claims)!;
        }

        private void AddUserToDb(User user)
        {
            _repo.Add(user);
        }
    }
}
