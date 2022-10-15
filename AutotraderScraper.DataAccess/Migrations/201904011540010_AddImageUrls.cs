namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageUrls : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImageUrls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        AdvertId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advert", t => t.AdvertId)
                .Index(t => t.AdvertId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageUrls", "AdvertId", "dbo.Advert");
            DropIndex("dbo.ImageUrls", new[] { "AdvertId" });
            DropTable("dbo.ImageUrls");
        }
    }
}
