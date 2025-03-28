
using Contacts.Models;
using Contacts.Services.Implementations;
using Contacts.Services.Interfaces;
using Contacts.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using IAuthenticationService = Contacts.Services.Interfaces.IAuthenticationService;
using AuthOptions = Contacts.AuthOptions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contacts.Repository.Interfaces;
using Newtonsoft.Json;

namespace Contacts.Controllers
{
    [Route("Auth")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _AuthService;
        public AuthenticationController(IAuthenticationService authService) { _AuthService = authService; }

        [HttpPost]
        [Route("Login")]
        public IActionResult SignIn(User userFromRequest)
        {
            var claims = _AuthService.SignIn(userFromRequest);

            if (!claims.IsSuccess)
            {
                TempData["ErrorField"]= claims.ErrorField;
                TempData["ErrorMessageFromServer"]= claims.ErrorMessage;

                return RedirectToAction("Index", "Home");
            }

            var token = JwtUtils.GetToken(claims.Value!);

            AppendTokenToCookie(Response, token);

            return RedirectToAction("Personal", "User");
        }
        [HttpGet]
        [Route("Registration")]
        public IActionResult RegisterPage()
        {
            return View("Views/Authentication/Register.cshtml");
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult RegisterAction(User userFromRequest)
        {
            _AuthService.SignOut(HttpContext);

            var claims = _AuthService.Registration(userFromRequest);

            if (!claims.IsSuccess)
            {
                ViewBag.ErrorField = claims.ErrorField;
                ViewBag.ErrorMessageFromServer = claims.ErrorMessage;

                return View("Views/Authentication/Register.cshtml");
            }

            var token = JwtUtils.GetToken(claims.Value!);

            AppendTokenToCookie(Response, token);

            return RedirectToAction("Personal", "User");
        }

        [Route("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            
            _AuthService.SignOut(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        private void AppendTokenToCookie(HttpResponse respone, string token)
        {
            respone.Cookies.Append(".AspNetCore.Application.Id", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }
    }
}
