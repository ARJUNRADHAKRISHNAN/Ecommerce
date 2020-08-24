namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImageLEngthChanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brands", "Image", c => c.String(maxLength: 250));
            AlterColumn("dbo.Items", "Image", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Items", "Image", c => c.String(maxLength: 500));
            AlterColumn("dbo.Brands", "Image", c => c.String(maxLength: 50));
        }
    }
}
