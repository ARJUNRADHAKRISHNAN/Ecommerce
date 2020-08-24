namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tabcreated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Image", c => c.String(maxLength: 250));
            AddColumn("dbo.Subcategories", "Image", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subcategories", "Image");
            DropColumn("dbo.Categories", "Image");
        }
    }
}
