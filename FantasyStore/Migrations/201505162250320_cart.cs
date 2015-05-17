namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Cart_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Cart_Id");
            AddForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Orders", new[] { "Cart_Id" });
            DropColumn("dbo.Orders", "Cart_Id");
        }
    }
}
