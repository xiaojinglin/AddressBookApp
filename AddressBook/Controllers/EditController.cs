using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class EditController : Controller
    {
        private Context _db = new Context();
        public EditController(Context db)
        {
            _db = db;
        }

        // GET: Edit
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            SetupGroupsSelectListItems();
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact contact)
        {
            var original = _db.Contacts.Find(contact.ContactId);
            var UpdatedContact = _db.Contacts
                .Where(c => c.Name.FirstName == contact.Name.FirstName && c.Name.LastName == contact.Name.LastName)
                .SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (UpdatedContact == null || UpdatedContact.ContactId == contact.ContactId)
                {
                    original.Name.FirstName = contact.Name.FirstName;
                    original.Name.LastName = contact.Name.LastName;
                    original.GroupId = contact.GroupId;
                    original.Phone = contact.Phone;
                    original.Address = contact.Address;
                    original.Email = contact.Email;
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The name you are going to update to is existed already");
            }

            SetupGroupsSelectListItems();
            return View(contact);
        }

        private void SetupGroupsSelectListItems()
        {
            ViewBag.GroupsSelectListItems = new SelectList(
                _db.Groups, "GroupId", "GroupName");
        }
    }
}