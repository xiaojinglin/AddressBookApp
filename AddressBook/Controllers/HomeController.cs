using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AddressBook.Models;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        private Context _db = new Context();
        public HomeController(Context db)
        {
            _db = db;
        }

        // GET: Contacts
        public ActionResult Index(int? GroupId, string search)
        {
            var contacts = _db.Contacts.Include(c => c.Group)
                .Include(c=>c.Name);

            if (GroupId!=null)
            {
                contacts = contacts.Where(c=>c.GroupId == GroupId);
            }

            if (!string.IsNullOrEmpty(search))
            {
                contacts = contacts.Where(c => c.Group.GroupName.Contains(search) ||
                                               c.Name.FirstName.Contains(search) ||
                                               c.Name.LastName.Contains(search) ||
                                               c.Phone.Contains(search) ||
                                               c.Address.Contains(search) ||
                                               c.Email.Contains(search));
                ViewBag.Search = search;

            }

            contacts = contacts
                        .OrderBy(c => c.Name.LastName)
                        .ThenBy(c => c.Name.FirstName);
            SetupGroupsSelectListItems();
            SetupGroups();
            return View(contacts.ToList());
        }

        public ActionResult Groups()
        {
            SetupGroups();
            return View();
        }

        [HttpPost]
        public ActionResult Groups(Group group)
        {
            if(group.GroupName!=null)
            {
                _db.Groups.Add(group);
                _db.SaveChanges();
                TempData["Message"] = "Your entry was successfully added!";
                return RedirectToAction("Groups", "Home");
            }
            SetupGroups();
            return View();

        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
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
            return View(contact);
        }

        private void SetupGroupsSelectListItems()
        {
            ViewBag.GroupsSelectListItems = new SelectList(
                _db.Groups, "GroupId", "GroupName");
        }

        private void SetupGroups()
        {
            ViewBag.Groups = _db.Groups;
        }
    }
}