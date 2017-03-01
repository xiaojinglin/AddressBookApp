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
        public ActionResult Index(int? GroupType, string search)
        {
            var contacts = _db.Contacts.Include(c => c.Group)
                .Include(c=>c.Name);
            //var contacts = _db.Contacts
            //                .OrderBy(c => c.Group.GroupName)
            //                .ThenBy(c => c.Name.LastName)
            //                .ThenBy(c => c.Name.FirstName);

            if (GroupType!=null)
            {
                contacts = contacts.Where(c=>c.GroupId == GroupType);
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
                        .OrderBy(c => c.Group.GroupName)
                        .ThenBy(c => c.Name.LastName)
                        .ThenBy(c => c.Name.FirstName);

            return View(contacts.ToList());
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
    }
}