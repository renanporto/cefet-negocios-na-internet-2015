namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Items", new[] { "Order_Id" });
            DropColumn("dbo.Items", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Order_Id", c => c.Int());
            CreateIndex("dbo.Items", "Order_Id");
            AddForeignKey("dbo.Items", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
