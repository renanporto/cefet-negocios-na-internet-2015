namespace FantasyStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart11111d : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WishLists", "Code", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WishLists", "Code", c => c.Guid(nullable: false));
        }
    }
}
