namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EcommItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Price = c.Single(nullable: false),
                        Image = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        BrandId = c.Long(nullable: false),
                        CategoryId = c.Long(nullable: false),
                        SubcategoryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: false)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: false)
                .ForeignKey("dbo.Subcategories", t => t.SubcategoryId, cascadeDelete: false)
                .Index(t => t.BrandId)
                .Index(t => t.CategoryId)
                .Index(t => t.SubcategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "SubcategoryId", "dbo.Subcategories");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Items", "BrandId", "dbo.Brands");
            DropIndex("dbo.Items", new[] { "SubcategoryId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropIndex("dbo.Items", new[] { "BrandId" });
            DropTable("dbo.Items");
        }
    }
}
