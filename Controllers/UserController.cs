using Contacts.Services.Implementations;
using Contacts.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Contacts.Controllers;

[Route("User")]
public class UserController : Controller
{
    private readonly IUserService _UserService;
    private readonly IContactService _ContactService;
    private readonly IContactBookService _ContactBookService;
    public UserController(IUserService userService, IContactService contactService, IContactBookService contactBookService) {
        _UserService = userService; 
        _ContactService = contactService;
        _ContactBookService = contactBookService;
    }

    [Authorize]
    public IActionResult Personal()
    {
        ViewBag.ContactBooks = _ContactBookService.GetContactBooksByUsername(User.Identity!.Name!);
        User user = _UserService.GetByUsername(User.Identity!.Name!);
        ViewBag.UserPersonal = user;

        ViewBag.ErrorField = TempData["ErrorField"];
        ViewBag.ErrorMessageFromServer = TempData["ErrorMessageFromServer"];
        ViewBag.Contacts = TempData["Contacts"];

        return View();
    }
}

