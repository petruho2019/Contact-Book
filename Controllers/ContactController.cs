using Contacts.Models.DTO;
using Contacts.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Controllers
{
    [Route("Contact")]
    [Authorize]
    public class ContactController : Controller
    {

        private readonly IContactService _Service;
        public ContactController(IContactService service) { _Service = service; }

        [Route("CreateContact")]
        [HttpPost]
        public IActionResult CreateContact(Contact contact)
        {
            string? contactBookName = Request.Query["contact_book_name"];

            if (contactBookName == null)
            {
                return RedirectToAction("Personal", "User");
            }

            _Service.Add(contact, contactBookName);
            return RedirectToAction("Contacts", "ContactBook", new { contact_book_name = contactBookName });
        }

        [HttpPost]
        [Route("Contacts")]
        public IActionResult ShowContactsByContactName(string contactName)
        {
            List<Contact> contacts = _Service.GetContactsByContactBookName(contactName);

            TempData["Contacts"] = contacts;

            return RedirectToAction("Personal", "User");
        }

        [HttpPost]
        [Route("Edit")]
        public IActionResult EditContact(Contact_ContactBookName contact_ContactBookName)
        {
            _Service.Edit(contact_ContactBookName.Contact);

            return RedirectToAction("Contacts", "ContactBook", new { contact_book_name = contact_ContactBookName.ContactBookName });
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteContact(Contact contact)
        {
            string? contactBookName = Request.Query["contact_book_name"];

            if (contactBookName == null)
            {
                return RedirectToAction("Personal", "User");
            }

            _Service.Delete(contact);

            return Ok();
        }
    }
}
