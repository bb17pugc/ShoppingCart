namespace ShoppingCart.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applydataannotations8 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "ShowProductPhoto_FileContents");
            DropColumn("dbo.Products", "ShowProductPhoto_ContentType");
            DropColumn("dbo.Products", "ShowProductPhoto_FileDownloadName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ShowProductPhoto_FileDownloadName", c => c.String());
            AddColumn("dbo.Products", "ShowProductPhoto_ContentType", c => c.String());
            AddColumn("dbo.Products", "ShowProductPhoto_FileContents", c => c.Binary());
        }
    }
}
