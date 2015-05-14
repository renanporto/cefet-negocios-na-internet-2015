namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image2355 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FieldValues", "Field_Id", "dbo.Fields");
            DropForeignKey("dbo.ProductFieldValues", "FieldValue_Id", "dbo.FieldValues");
            DropForeignKey("dbo.ProductFieldValues", "Product_Id", "dbo.Products");
            DropIndex("dbo.FieldValues", new[] { "Field_Id" });
            DropIndex("dbo.ProductFieldValues", new[] { "FieldValue_Id" });
            DropIndex("dbo.ProductFieldValues", new[] { "Product_Id" });
            DropTable("dbo.Fields");
            DropTable("dbo.FieldValues");
            DropTable("dbo.ProductFieldValues");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductFieldValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FieldValue_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FieldValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Field_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Fields",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProductFieldValues", "Product_Id");
            CreateIndex("dbo.ProductFieldValues", "FieldValue_Id");
            CreateIndex("dbo.FieldValues", "Field_Id");
            AddForeignKey("dbo.ProductFieldValues", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.ProductFieldValues", "FieldValue_Id", "dbo.FieldValues", "Id");
            AddForeignKey("dbo.FieldValues", "Field_Id", "dbo.Fields", "Id");
        }
    }
}
