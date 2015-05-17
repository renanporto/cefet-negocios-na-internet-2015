namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart2323 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreditCardNumber = c.String(),
                        ExpirationDate = c.DateTime(nullable: false),
                        SecurityCode = c.String(),
                        CartCode = c.String(),
                        Value = c.Decimal(precision: 18, scale: 2),
                        Installment = c.Int(nullable: false),
                        InstallmentValue = c.Decimal(precision: 18, scale: 2),
                        Cart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Carts", t => t.Cart_Id)
                .Index(t => t.Cart_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "Cart_Id", "dbo.Carts");
            DropIndex("dbo.Payments", new[] { "Cart_Id" });
            DropTable("dbo.Payments");
        }
    }
}
