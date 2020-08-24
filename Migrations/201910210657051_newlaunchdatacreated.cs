namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newlaunchdatacreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Newlaunchproducts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ItemId = c.Long(nullable: false),
                        NewlaunchId = c.Long(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Newlaunchproducts", "ItemId", "dbo.Items");
            DropIndex("dbo.Newlaunchproducts", new[] { "ItemId" });
            DropTable("dbo.Newlaunchproducts");
        }
    }
}
