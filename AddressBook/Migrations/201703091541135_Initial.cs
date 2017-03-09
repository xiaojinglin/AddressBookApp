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
                        GroupId = c.Int(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(),
                        Email = c.String(),
                        Name_NameId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Group", t => t.GroupId)
                .ForeignKey("dbo.Name", t => t.Name_NameId)
                .Index(t => t.GroupId)
                .Index(t => t.Name_NameId);
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Name",
                c => new
                    {
                        NameId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.NameId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contact", "Name_NameId", "dbo.Name");
            DropForeignKey("dbo.Contact", "GroupId", "dbo.Group");
            DropIndex("dbo.Contact", new[] { "Name_NameId" });
            DropIndex("dbo.Contact", new[] { "GroupId" });
            DropTable("dbo.Name");
            DropTable("dbo.Group");
            DropTable("dbo.Contact");
        }
    }
}
