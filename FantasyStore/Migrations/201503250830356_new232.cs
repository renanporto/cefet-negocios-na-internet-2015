namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new232 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishListVariants",
                c => new
                    {
                        WishList_Id = c.Int(nullable: false),
                        Variant_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WishList_Id, t.Variant_Id })
                .ForeignKey("dbo.WishLists", t => t.WishList_Id, cascadeDelete: true)
                .ForeignKey("dbo.Variants", t => t.Variant_Id, cascadeDelete: true)
                .Index(t => t.WishList_Id)
                .Index(t => t.Variant_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishListVariants", "Variant_Id", "dbo.Variants");
            DropForeignKey("dbo.WishListVariants", "WishList_Id", "dbo.WishLists");
            DropIndex("dbo.WishListVariants", new[] { "Variant_Id" });
            DropIndex("dbo.WishListVariants", new[] { "WishList_Id" });
            DropTable("dbo.WishListVariants");
        }
    }
}
