namespace AddressBook.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AddressBook.Models.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AddressBook.Models.Context context)
        {
            var NameJohnDoe = new Name() { NameId = 1, FirstName = "John", LastName = "Doe" };
            var NameLilyWang = new Name() { NameId = 2, FirstName = "Lily", LastName = "Wang" };
            var NameJohnChen = new Name() { NameId = 3, FirstName = "John", LastName = "Chen" };

            context.Names.AddOrUpdate(
                n => new { n.FirstName, n.LastName },
                NameJohnDoe,
                NameLilyWang,
                NameJohnChen
            );

            var groupFamily = new Group(Group.GroupType.Family);
            var groupFriend = new Group(Group.GroupType.Friend);
            var groupColleague = new Group(Group.GroupType.Colleague);
            var groupSchoolmate = new Group(Group.GroupType.Schoolmate);
            var groupStranger = new Group(Group.GroupType.Stranger);

            context.Groups.AddOrUpdate(
                g => g.GroupName,
                groupFamily,
                groupFriend,
                groupColleague,
                groupSchoolmate,
                groupStranger
                );

            var contact1 = new Contact()
            {
                ContactId = 1,
                GroupId = groupFamily.GroupId,
                Phone = "5022222222",
                Address = "Usa",
                Email = "JohnDoe@gmail.com",
                Name = NameJohnDoe,

            };

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact1);

            var contact2 = new Contact()
            {
                ContactId = 2,
                GroupId = groupFriend.GroupId,
                Phone = "5022222345",
                Address = "Usa",
                Email = "LilyWang@gmail.com",
                Name = NameLilyWang,
            };
            

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact2);

            var contact3 = new Contact()
            {
                ContactId = 3,
                GroupId = groupSchoolmate.GroupId,
                Phone = "5022222678",
                Address = "China",
                Email = "JohnChen@gmail.com",
                Name = NameJohnChen,
            };

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact3);
        }
    }
}
