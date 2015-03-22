namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Owner_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Owner_Id");
            AddForeignKey("dbo.Orders", "Owner_Id", "dbo.Accounts", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Owner_Id", "dbo.Accounts");
            DropIndex("dbo.Orders", new[] { "Owner_Id" });
            DropColumn("dbo.Orders", "Owner_Id");
            DropTable("dbo.Accounts");
        }
    }
}
