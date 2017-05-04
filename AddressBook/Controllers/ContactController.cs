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
    public class ContactController : Controller
    {
        private Context _db = new Context();

        //Adding a constructor that requires an instance of the DbContext
        public ContactController(Context db)
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


        // GET: Add
        public ActionResult Add()
        {
            SetupGroupsSelectListItems();
            return View();
        }

        // POST: Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Contact contact)
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
                return RedirectToAction("Index");

            }

            if (newName != null)
            {
                ModelState.AddModelError("", "The name you enter is existed already");
            }

            SetupGroupsSelectListItems();
            return View(contact);
        }


        // GET: Edit
        public ActionResult Edit(int? id)
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

        // Post: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            //Get the original contact from the database
            var original = _db.Contacts.Find(contact.ContactId);

            //Get the contact from the database by name
            var UpdatedContact = _db.Contacts
                .Where(c => c.Name.FirstName == contact.Name.FirstName && c.Name.LastName == contact.Name.LastName)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                //The new name isn't confilct with the existing names or the contact name didn't change
                if (UpdatedContact == null || UpdatedContact.ContactId == contact.ContactId)
                {
                    //Update contact
                    original.Name.FirstName = contact.Name.FirstName;
                    original.Name.LastName = contact.Name.LastName;
                    original.GroupId = contact.GroupId;
                    original.Phone = contact.Phone;
                    original.Address = contact.Address;
                    original.Email = contact.Email;
                    _db.SaveChanges();
                    TempData["Message"] = "Your entry was successfully updated!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "The name you are going to update to is existed already");
            }

            SetupGroupsSelectListItems();
            return View(contact);
        }


        // GET: Delete
        public ActionResult Delete(int? id)
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

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Find the contact and remove the name and then remove the contact
            Contact contact = _db.Contacts.Find(id);
            _db.Names.Remove(contact.Name);
            _db.Contacts.Remove(contact);
            _db.SaveChanges();
            TempData["Message"] = "Your entry was successfully deleted!";
            return RedirectToAction("Index");
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