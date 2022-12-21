namespace Pramod.GuestTracker.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AttendanceEntityAndPhonenumberFieldAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArrivedAt = c.DateTime(nullable: false),
                        NumberOfPeople = c.Int(nullable: false),
                        Guest_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guests", t => t.Guest_Id)
                .Index(t => t.Guest_Id);
            
            AddColumn("dbo.Guests", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendences", "Guest_Id", "dbo.Guests");
            DropIndex("dbo.Attendences", new[] { "Guest_Id" });
            DropColumn("dbo.Guests", "Phone");
            DropTable("dbo.Attendences");
        }
    }
}
