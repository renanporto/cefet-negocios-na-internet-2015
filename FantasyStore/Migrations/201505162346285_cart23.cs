namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Clients", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Clients", new[] { "User_Id" });
            DropTable("dbo.Clients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Document = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Clients", "User_Id");
            AddForeignKey("dbo.Clients", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
