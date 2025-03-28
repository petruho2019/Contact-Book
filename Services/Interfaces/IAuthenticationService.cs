using System.Security.Claims;

namespace Contacts.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Result<List<Claim>> SignIn(User user);
        void SignOut(HttpContext context);
        Result<List<Claim>> Registration(User user);
    }
}
