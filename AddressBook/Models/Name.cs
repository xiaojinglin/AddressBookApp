using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Name
    {
        [Key]
        public int NameId { get; set; }

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
}