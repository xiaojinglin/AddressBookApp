using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class ContactGroup
    {
        public int ContactGroupId { get; set; }
        public int ContactId { get; set; }
        public int GroupId { get; set; }

        public Contact Contact { get; set; }
        public Group Group { get; set; }

    }
}