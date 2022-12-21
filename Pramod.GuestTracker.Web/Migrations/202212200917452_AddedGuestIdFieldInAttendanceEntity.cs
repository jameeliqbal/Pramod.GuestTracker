namespace Pramod.GuestTracker.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGuestIdFieldInAttendanceEntity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Attendences", "Guest_Id", "dbo.Guests");
            DropIndex("dbo.Attendences", new[] { "Guest_Id" });
            RenameColumn(table: "dbo.Attendences", name: "Guest_Id", newName: "GuestId");
            AlterColumn("dbo.Attendences", "GuestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendences", "GuestId");
            AddForeignKey("dbo.Attendences", "GuestId", "dbo.Guests", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendences", "GuestId", "dbo.Guests");
            DropIndex("dbo.Attendences", new[] { "GuestId" });
            AlterColumn("dbo.Attendences", "GuestId", c => c.Int());
            RenameColumn(table: "dbo.Attendences", name: "GuestId", newName: "Guest_Id");
            CreateIndex("dbo.Attendences", "Guest_Id");
            AddForeignKey("dbo.Attendences", "Guest_Id", "dbo.Guests", "Id");
        }
    }
}
