using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Contact
    {
        public Contact()
        {
            Groups = new List<Group>();
        }
        [Key]
        public int ContactId { get; set; }
        public int NameId { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number is invalid")]
        public string Phone { get; set; }

        public string  Address { get; set; }

        [RegularExpression(@"^\s*([A-Za-z0-9_-]+(\.\w+)*@([\w-]+\.)+\w{2,3})\s*$", ErrorMessage = "Email is invalid")]
        public string  Email { get; set; }

        public virtual Name Name { get; set; }


        public virtual ICollection<Group> Groups { get; set; }

        public string GroupNames
        {
            get
            {
                var groupNamesDisplayText = "";
                var groupNames = Groups.Select(g => g.GroupName).ToList();
                groupNamesDisplayText = string.Join(", ", groupNames);
                return groupNamesDisplayText;
            }
        }

        public void AddGroup(Group group)
        {
            Groups.Add(group);
        }
        
    }
}