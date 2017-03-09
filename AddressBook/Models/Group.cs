using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Group
    {
        public Group()
        {
            contacts = new List<Contact>();
        }

        public Group(int groundId, string groundName)
        {
            GroupId = groundId;
            GroupName = groundName;
        }

        [Key]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Please enter a group name")]
        public string GroupName { get; set; }
        
        public ICollection<Contact> contacts { get; set; }
    }
}