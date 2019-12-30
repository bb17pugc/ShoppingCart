namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrdersDatas", "ProductsCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrdersDatas", "ProductsCount");
        }
    }
}
