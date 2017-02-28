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
        public ActionResult Index()
        {
            var contacts = _db.Contacts
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

        //// GET: Contacts/Create
        //public ActionResult Create()
        //{

        //    SetupGroupsSelectListItems();
        //    return View();
        //}

        //// POST: Contacts/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Contact contact)
        //{
        //    var newName = db.Names
        //            .Where(n => (n.FirstName == contact.Name.FirstName && n.LastName == contact.Name.LastName))
        //            .SingleOrDefault();

        //    if (ModelState.IsValid && newName == null)
        //    {
        //        db.Contacts.Add(contact);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");

        //    }
        //    if (newName != null)
        //    {
        //        ModelState.AddModelError("", "The name you enter is existed already");
        //    }
        //    SetupGroupsSelectListItems();
        //    return View(contact);
        //}

        //// GET: Contacts/Edit/5 
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = db.Contacts.Find(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    SetupGroupsSelectListItems();
        //    return View(contact);
        //}

        //// POST: Contacts/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Contact contact)
        //{
        //    var original = db.Contacts.Find(contact.ContactId);
        //    var UpdatedContact = db.Contacts
        //        .Where(c => c.Name.FirstName == contact.Name.FirstName && c.Name.LastName == contact.Name.LastName)
        //        .SingleOrDefault();
        //    if (ModelState.IsValid)
        //    {
        //        if (UpdatedContact == null || UpdatedContact.ContactId == contact.ContactId)
        //        {
        //            original.Name.FirstName = contact.Name.FirstName;
        //            original.Name.LastName = contact.Name.LastName;
        //            original.GroupId = contact.GroupId;
        //            original.Phone = contact.Phone;
        //            original.Address = contact.Address;
        //            original.Email = contact.Email;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        ModelState.AddModelError("", "The name you are going to update to is existed already");
        //    }

        //    SetupGroupsSelectListItems();
        //    return View(contact);
        //}

        //// GET: Contacts/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Contact contact = db.Contacts.Find(id);
        //    if (contact == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contact);
        //}

        //// POST: Contacts/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Contact contact = db.Contacts.Find(id);
        //    db.Names.Remove(contact.Name);
        //    db.Contacts.Remove(contact);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private void SetupGroupsSelectListItems()
        {
            ViewBag.GroupsSelectListItems = new SelectList(
                _db.Groups, "GroupId", "GroupName");
        }
    }
}