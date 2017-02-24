using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public enum GroupName
    {
        Family = 1,
        Friend = 2,
        Colleague = 3,
        Schoolmate = 4,
        Stranger = 5
    }
    public class Group
    {
        public Group()
        {
            Contacts = new List<Contact>();
        }
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}