namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesModels", "OrderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesModels", "OrderId", c => c.String());
        }
    }
}
