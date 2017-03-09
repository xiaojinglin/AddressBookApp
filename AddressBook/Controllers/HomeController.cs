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

        //Adding a constructor that requires an instance of the DbContext
        public HomeController(Context db)
        {
            _db = db;
        }

        // GET:Home/Index
        public ActionResult Index(int? GroupId, string search)
        {
            var contacts = _db.Contacts.Include(c => c.Group)
                .Include(c=>c.Name);

            //Filter the contacts by group
            if (GroupId!=null)
            {
                contacts = contacts.Where(c=>c.GroupId == GroupId);
            }

            //Search contacts
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

            //Order the contacts
            contacts = contacts
                        .OrderBy(c => c.Name.LastName)
                        .ThenBy(c => c.Name.FirstName);

            //get the select list of groups
            SetupGroupsSelectListItems();

            //get groups
            SetupGroups();

            return View(contacts.ToList());
        }       

        //GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Get the contact
            Contact contact = _db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }     

        //A function to set up groups SelectList
        private void SetupGroupsSelectListItems()
        {
            ViewBag.GroupsSelectListItems = new SelectList(
                _db.Groups, "GroupId", "GroupName");
        }

        //A function to get set up groups
        private void SetupGroups()
        {
            ViewBag.Groups = _db.Groups;
        }
    }
}