namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductPhoto", c => c.Binary());
            DropColumn("dbo.Products", "UserPhoto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "UserPhoto", c => c.Binary());
            DropColumn("dbo.Products", "ProductPhoto");
        }
    }
}
