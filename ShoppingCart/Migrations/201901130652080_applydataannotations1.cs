namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SalesModels");
            AddColumn("dbo.SalesModels", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.SalesModels", "OrderId", c => c.String());
            AddPrimaryKey("dbo.SalesModels", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SalesModels");
            AlterColumn("dbo.SalesModels", "OrderId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.SalesModels", "ID");
            AddPrimaryKey("dbo.SalesModels", "OrderId");
        }
    }
}
