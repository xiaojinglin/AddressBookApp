using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class GroupController : Controller
    {
        private Context _db = new Context();

        //Adding a constructor that requires an instance of the DbContext
        public GroupController(Context db)
        {
            _db = db;
        }

        //GET: Home/Groups
        public ActionResult Index()
        {
            SetupGroups();
            return View();
        }

        //POST: Home/Groups
        [HttpPost]
        public ActionResult AddGroup(Group group)
        {
            //Check whether there is a group name
            if (group.GroupName != null)
            {
                //Insert the new group to the database
                _db.Groups.Add(group);
                _db.SaveChanges();
                TempData["Message"] = "Your entry was successfully added!";
                return RedirectToAction("Index");
            }

            //Return to the group page if the group name is null
            SetupGroups();
            return View();

        }        

        // GET: Edit
        public ActionResult GroupEdit(int? GroupId)
        {
            if (GroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = _db.Groups.Find(GroupId);
            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        // Post: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GroupEdit(Group group)
        {
            //Get the original group from the database
            var original = _db.Groups.Find(group.GroupId);

            //Get the group from the database by name
            var UpdatedGroup = _db.Groups
                .Where(g => g.GroupName == group.GroupName)
                .SingleOrDefault();

            if (ModelState.IsValid)
            {
                //costomer didn't change the name or the new name isn't confilct with the existing names
                if (UpdatedGroup == null || UpdatedGroup.GroupId == group.GroupId)
                {
                    //Update group
                    original.GroupName = group.GroupName;
                    _db.SaveChanges();
                    TempData["Message"] = "Your entry was successfully updated!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "The name you are going to update to is existed already");
            }
            
            return View(group);
        }

        // GET: Delete
        public ActionResult GroupDelete(int? GroupId)
        {
            if (GroupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Group group = _db.Groups.Find(GroupId);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Delete
        [HttpPost, ActionName("GroupDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult GroupDeleteConfirmed(int GroupId)
        {
            //Find the group and and the contacts in the group
            Group group = _db.Groups.Find(GroupId);
            var contacts = _db.Contacts.Where(c => c.GroupId == GroupId);
            //Move the contact from current group to ungrouped group
            foreach (var c in contacts)
            {
                c.GroupId = 6;
            }
            //Remove the group
            _db.Groups.Remove(group);
            _db.SaveChanges();
            TempData["Message"] = "Your entry was successfully deleted!";
            return RedirectToAction("Index");
        }

        //A function to get set up groups
        private void SetupGroups()
        {
            ViewBag.Groups = _db.Groups;
        }
    }
}