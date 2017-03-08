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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact contact)
        {
            var newName = _db.Names
                    .Where(n => (n.FirstName == contact.Name.FirstName && n.LastName == contact.Name.LastName))
                    .SingleOrDefault();

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