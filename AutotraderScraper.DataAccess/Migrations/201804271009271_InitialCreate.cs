namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarMake",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Name = c.String(),
                        CarMakeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarMake", t => t.CarMakeId)
                .Index(t => t.CarMakeId);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PriceTag = c.String(),
                        Link = c.String(),
                        Thumbnail = c.String(),
                        CarModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarModel", t => t.CarModelId)
                .Index(t => t.CarModelId);
            
            CreateTable(
                "dbo.ArticleVersion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        Version = c.Int(nullable: false),
                        Title = c.String(),
                        Teaser = c.String(),
                        Description = c.String(),
                        Year = c.Int(nullable: false),
                        BodyType = c.String(),
                        Mileage = c.Int(),
                        TransmissionType = c.String(),
                        EngineSize = c.Double(nullable: false),
                        Bhp = c.Int(),
                        FuelType = c.String(),
                        SellerType = c.String(),
                        Location = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .Index(t => t.ArticleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "CarModelId", "dbo.CarModel");
            DropForeignKey("dbo.ArticleVersion", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.CarModel", "CarMakeId", "dbo.CarMake");
            DropIndex("dbo.ArticleVersion", new[] { "ArticleId" });
            DropIndex("dbo.Article", new[] { "CarModelId" });
            DropIndex("dbo.CarModel", new[] { "CarMakeId" });
            DropTable("dbo.ArticleVersion");
            DropTable("dbo.Article");
            DropTable("dbo.CarModel");
            DropTable("dbo.CarMake");
        }
    }
}
