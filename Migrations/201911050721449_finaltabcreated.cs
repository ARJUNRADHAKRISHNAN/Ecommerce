namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finaltabcreated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Bookings", "ItemId");
            AddForeignKey("dbo.Bookings", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ItemId", "dbo.Items");
            DropIndex("dbo.Bookings", new[] { "ItemId" });
        }
    }
}
