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
            
        }
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "Please select a group")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "Please enter a phone number")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Phone number is invalid")]
        public string Phone { get; set; }

        public string  Address { get; set; }

        [RegularExpression(@"^\s*([A-Za-z0-9_-]+(\.\w+)*@([\w-]+\.)+\w{2,3})\s*$", ErrorMessage = "Email is invalid")]
        public string  Email { get; set; }
        
        public virtual Name Name { get; set; }

        
        public virtual Group Group { get; set; }


    }
}