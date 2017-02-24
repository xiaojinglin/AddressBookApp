namespace AddressBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        NameId = c.Int(nullable: false),
                        Phone = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Name", t => t.NameId)
                .Index(t => t.NameId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Name",
                c => new
                    {
                        NameId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.NameId);
            
            CreateTable(
                "dbo.GroupContact",
                c => new
                    {
                        Group_GroupId = c.Int(nullable: false),
                        Contact_ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_GroupId, t.Contact_ContactId })
                .ForeignKey("dbo.Group", t => t.Group_GroupId)
                .ForeignKey("dbo.Contact", t => t.Contact_ContactId)
                .Index(t => t.Group_GroupId)
                .Index(t => t.Contact_ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "NameId", "dbo.Name");
            DropForeignKey("dbo.GroupContact", "Contact_ContactId", "dbo.Contact");
            DropForeignKey("dbo.GroupContact", "Group_GroupId", "dbo.Group");
            DropIndex("dbo.GroupContact", new[] { "Contact_ContactId" });
            DropIndex("dbo.GroupContact", new[] { "Group_GroupId" });
            DropIndex("dbo.Contact", new[] { "NameId" });
            DropTable("dbo.GroupContact");
            DropTable("dbo.Name");
            DropTable("dbo.Group");
            DropTable("dbo.Contact");
        }
    }
}
