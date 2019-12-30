namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ShowProductPhoto_FileContents", c => c.Binary());
            AddColumn("dbo.Products", "ShowProductPhoto_ContentType", c => c.String());
            AddColumn("dbo.Products", "ShowProductPhoto_FileDownloadName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ShowProductPhoto_FileDownloadName");
            DropColumn("dbo.Products", "ShowProductPhoto_ContentType");
            DropColumn("dbo.Products", "ShowProductPhoto_FileContents");
        }
    }
}
