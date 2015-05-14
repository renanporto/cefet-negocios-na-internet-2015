namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image222232 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CollectionProducts", newName: "ProductCollections");
            DropPrimaryKey("dbo.ProductCollections");
            AddPrimaryKey("dbo.ProductCollections", new[] { "Product_Id", "Collection_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductCollections");
            AddPrimaryKey("dbo.ProductCollections", new[] { "Collection_Id", "Product_Id" });
            RenameTable(name: "dbo.ProductCollections", newName: "CollectionProducts");
        }
    }
}
