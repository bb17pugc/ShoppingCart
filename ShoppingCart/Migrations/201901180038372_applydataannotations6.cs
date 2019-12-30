namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UserPhoto", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UserPhoto");
        }
    }
}
