namespace Pramod.GuestTracker.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRemarksPropertyToWAMessageStatusEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WAMessageStatus", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WAMessageStatus", "Remarks");
        }
    }
}
