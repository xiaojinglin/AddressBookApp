using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class AddController : Controller
    {
        private Context _db = new Context();
        public AddController(Context db)
        {
            _db = db;
        }
        // GET: Add
        public ActionResult Index()
        {
            SetupGroupsSelectListItems();
            return View();
        }

        // POST: Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact contact)
        {
            //Search the new name from the existing data
            var newName = _db.Names
                    .Where(n => (n.FirstName == contact.Name.FirstName && n.LastName == contact.Name.LastName))
                    .SingleOrDefault();

            //Insert the new contact to the database
            if (ModelState.IsValid && newName == null)
            {
                _db.Contacts.Add(contact);
                _db.SaveChanges();
                TempData["Message"] = "Your entry was successfully added!";
                return RedirectToAction("Index", "Home");

            }

            if (newName != null)
            {
                ModelState.AddModelError("", "The name you enter is existed already");
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