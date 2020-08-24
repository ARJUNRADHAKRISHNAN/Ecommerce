namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newlaunchtabcreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Newlaunches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Image = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Newlaunches");
        }
    }
}
