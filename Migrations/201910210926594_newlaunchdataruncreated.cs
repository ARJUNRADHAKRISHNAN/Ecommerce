namespace Ecommerce.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newlaunchdataruncreated : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Newlaunchproducts", "NewlaunchId");
            AddForeignKey("dbo.Newlaunchproducts", "NewlaunchId", "dbo.Newlaunches", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Newlaunchproducts", "NewlaunchId", "dbo.Newlaunches");
            DropIndex("dbo.Newlaunchproducts", new[] { "NewlaunchId" });
        }
    }
}
