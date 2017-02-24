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

            const int GroupIdFamily = (int)GroupName.Family;
            const int GroupIdFriend = (int)GroupName.Friend;
            const int GroupIdColleague = (int)GroupName.Colleague;
            const int GroupIdSchoolmate = (int)GroupName.Schoolmate;
            const int GroupIdStranger = (int)GroupName.Stranger;
            var groupFamily = new Group() { GroupId = GroupIdFamily, GroupName = GroupName.Family.ToString() };
            var groupFriend = new Group() { GroupId = GroupIdFriend, GroupName = GroupName.Friend.ToString() };
            var groupColleague = new Group() { GroupId = GroupIdColleague, GroupName = GroupName.Colleague.ToString() };
            var groupSchoolmate = new Group() { GroupId = GroupIdSchoolmate, GroupName = GroupName.Schoolmate.ToString() };
            var groupStranger = new Group() { GroupId = GroupIdStranger, GroupName = GroupName.Stranger.ToString() };

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
                NameId = 1,
                Phone = "5022222222",
                Address = "Usa",
                Email = "JohnDoe@gmail.com",
                
            };
            contact1.AddGroup(groupFamily);

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact1);

            var contact2 = new Contact()
            {
                ContactId = 2,
                NameId = 2,
                Phone = "5022222345",
                Address = "Usa",
                Email = "LilyWang@gmail.com"
            };

            contact2.AddGroup(groupFriend);

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact2);

            var contact3 = new Contact()
            {
                ContactId = 3,
                NameId = 3,
                Phone = "5022222678",
                Address = "China",
                Email = "JohnChen@gmail.com"
            };

            contact3.AddGroup(groupFriend);
            contact3.AddGroup(groupSchoolmate);

            context.Contacts.AddOrUpdate(
                c => c.ContactId,
                contact3);
        }
    }
}
