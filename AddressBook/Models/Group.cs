using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public class Group
    {
        public enum GroupType

        {
            Family,
            Friend,
            Colleague,
            Schoolmate,
            Stranger
        }

        public Group()
        {
        }

        public Group(GroupType groundType, string groundName = null)
        {
            GroupId = (int)groundType;

            // If we don't have a groundName argument, 
            // then use the string representation of the grounp type for the groundName.
            GroupName = groundName ?? groundType.ToString();
        }
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        
    }
}