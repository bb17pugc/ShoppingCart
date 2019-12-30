namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrdersDatas", "OrderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrdersDatas", "OrderId", c => c.String());
        }
    }
}
