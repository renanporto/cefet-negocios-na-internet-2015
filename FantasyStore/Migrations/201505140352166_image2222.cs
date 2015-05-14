namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image2222 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CollectionProducts",
                c => new
                    {
                        Collection_Id = c.Int(nullable: false),
                        Product_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Collection_Id, t.Product_Id })
                .ForeignKey("dbo.Collections", t => t.Collection_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Collection_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CollectionProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CollectionProducts", "Collection_Id", "dbo.Collections");
            DropIndex("dbo.CollectionProducts", new[] { "Product_Id" });
            DropIndex("dbo.CollectionProducts", new[] { "Collection_Id" });
            DropTable("dbo.CollectionProducts");
            DropTable("dbo.Collections");
        }
    }
}
