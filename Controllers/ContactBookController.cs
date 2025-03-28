using Contacts.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contacts.Controllers
{
    [Route("ContactBook")]
    [Authorize]
    public class ContactBookController : Controller
    {
        private readonly IContactBookService _ContactBookService;
        public ContactBookController(IContactBookService contactBookService) { _ContactBookService = contactBookService; }

        [Route("Contacts")]
        [HttpGet]
        public IActionResult Contacts()
        {
            string? contactBookName = Request.Query["contact_book_name"];

            if (contactBookName == null)
            {
                return RedirectToAction("Personal", "User");
            }

            ViewBag.ContactBName = contactBookName!;
            ViewBag.Contacts = _ContactBookService.GetContactsByContactBookName(contactBookName);

            return View("/Views/ContactBook/Contacts.cshtml");
        }

        [HttpPost]
        public IActionResult CreateContactBook(ContactBook contactBook)
        {
            var result = _ContactBookService.CreateContactBook(contactBook, HttpContext.User.Identity!.Name!);

            if (!result.IsSuccess)
            {
                TempData["ErrorField"] = result.ErrorField;
                TempData["ErrorMessageFromServer"] = result.ErrorMessage;

                return RedirectToAction("Contacts", "Contact");
            }
            
            return RedirectToAction("Personal", "User");
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteContactBook(string nameContactBook)
        {
            _ContactBookService.Delete(nameContactBook);

            return Ok();
        }
    }
}
