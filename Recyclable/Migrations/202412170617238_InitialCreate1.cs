namespace Recyclable.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RecyclableItems", "ItemDescription", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecyclableItems", "ItemDescription", c => c.String(maxLength: 150));
        }
    }
}
