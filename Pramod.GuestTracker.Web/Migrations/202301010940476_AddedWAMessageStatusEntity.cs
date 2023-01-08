namespace Pramod.GuestTracker.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWAMessageStatusEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WAMessageStatus",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PhoneNumber = c.String(),
                        Type = c.String(),
                        Timestamp = c.String(),
                        ConversationId = c.String(),
                        ConversationOrigin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WAMessageStatus");
        }
    }
}
