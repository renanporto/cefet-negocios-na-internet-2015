namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart11111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WishLists", "Name", c => c.String());
            AddColumn("dbo.WishLists", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.WishLists", "User_Id");
            AddForeignKey("dbo.WishLists", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.WishLists", new[] { "User_Id" });
            DropColumn("dbo.WishLists", "User_Id");
            DropColumn("dbo.WishLists", "Name");
        }
    }
}
