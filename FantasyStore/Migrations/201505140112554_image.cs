namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Name = c.String(),
                        Variant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Variant_Id)
                .Index(t => t.Variant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Variant_Id", "dbo.Products");
            DropIndex("dbo.Images", new[] { "Variant_Id" });
            DropTable("dbo.Images");
        }
    }
}
