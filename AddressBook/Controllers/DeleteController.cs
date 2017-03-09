using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class DeleteController : Controller
    {
        private Context _db = new Context();
        public DeleteController(Context db)
        {
            _db = db;
        }

        // GET: Delete
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
            return View(contact);
        }

        // POST: Delete
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Find the contact and remove the name and then remove the contact
            Contact contact = _db.Contacts.Find(id);
            _db.Names.Remove(contact.Name);
            _db.Contacts.Remove(contact);
            _db.SaveChanges();
            TempData["Message"] = "Your entry was successfully deleted!";
            return RedirectToAction("Index", "Home");
        }
    }
}