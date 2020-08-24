namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EcommItemfeatures1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Itemfeatures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Description = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        ItemId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.ItemId, cascadeDelete: false)
                .Index(t => t.ItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Itemfeatures", "ItemId", "dbo.Items");
            DropIndex("dbo.Itemfeatures", new[] { "ItemId" });
            DropTable("dbo.Itemfeatures");
        }
    }
}
