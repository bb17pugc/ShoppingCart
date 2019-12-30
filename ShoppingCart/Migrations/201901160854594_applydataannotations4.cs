namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdersDatas", "OrderId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdersDatas", "OrderId");
        }
    }
}
