namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fieldvalues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductFieldValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Field = c.String(),
                        Value = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductFieldValues", "Product_Id", "dbo.Products");
            DropIndex("dbo.ProductFieldValues", new[] { "Product_Id" });
            DropTable("dbo.ProductFieldValues");
        }
    }
}
