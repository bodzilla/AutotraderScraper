namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiArticle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        ArticleId = c.Int(nullable: false),
                        AutotraderResponseId = c.Int(nullable: false),
                        MotResponseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .ForeignKey("dbo.AutotraderResponse", t => t.AutotraderResponseId)
                .ForeignKey("dbo.MotResponse", t => t.MotResponseId)
                .Index(t => t.ArticleId)
                .Index(t => t.AutotraderResponseId)
                .Index(t => t.MotResponseId);
            
            CreateTable(
                "dbo.AutotraderResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        VehicleId = c.Int(nullable: false),
                        AdvertId = c.Int(nullable: false),
                        SellerId = c.Int(nullable: false),
                        FinanceId = c.Int(nullable: false),
                        PageDataId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Advert", t => t.AdvertId)
                .ForeignKey("dbo.Finance", t => t.FinanceId)
                .ForeignKey("dbo.PageData", t => t.PageDataId)
                .ForeignKey("dbo.Seller", t => t.SellerId)
                .ForeignKey("dbo.Vehicle", t => t.VehicleId)
                .Index(t => t.VehicleId)
                .Index(t => t.AdvertId)
                .Index(t => t.SellerId)
                .Index(t => t.FinanceId)
                .Index(t => t.PageDataId);
            
            CreateTable(
                "dbo.Advert",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        InstantMessagingId = c.Int(nullable: false),
                        SocialMediaLinksId = c.Int(nullable: false),
                        Description = c.String(),
                        ManufacturerApproved = c.Boolean(),
                        Price = c.String(),
                        PriceValuation = c.String(),
                        Title = c.String(),
                        AttentionGrabber = c.String(),
                        MainImageUrl = c.String(),
                        Products = c.String(),
                        StockRevisionNumber = c.Int(),
                        Channel = c.String(),
                        AdvertSaved = c.Boolean(),
                        ShowDealBuilder = c.Boolean(),
                        IsPartExAvailable = c.Boolean(),
                        HasWarrantyDirect = c.Boolean(),
                        HasGalleryDealerBanner = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InstantMessaging", t => t.InstantMessagingId)
                .ForeignKey("dbo.SocialMediaLinks", t => t.SocialMediaLinksId)
                .Index(t => t.InstantMessagingId)
                .Index(t => t.SocialMediaLinksId);
            
            CreateTable(
                "dbo.InstantMessaging",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        PreferencesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Preferences", t => t.PreferencesId)
                .Index(t => t.PreferencesId);
            
            CreateTable(
                "dbo.Preferences",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        ChatEnabled = c.Boolean(),
                        TextEnabled = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialMediaLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Twitter = c.String(),
                        Facebook = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Finance",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Price = c.String(),
                        Provider = c.String(),
                        ProviderName = c.String(),
                        Type = c.String(),
                        CalculatorLink = c.String(),
                        CanCalculateMonthlyCost = c.Boolean(),
                        InitialDeposit = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        OdsId = c.Int(nullable: false),
                        TrackingId = c.Int(nullable: false),
                        Title = c.String(),
                        Canonical = c.String(),
                        SkipPageViewTracking = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ods", t => t.OdsId)
                .ForeignKey("dbo.Tracking", t => t.TrackingId)
                .Index(t => t.OdsId)
                .Index(t => t.TrackingId);
            
            CreateTable(
                "dbo.Ods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        AdvertId = c.Long(),
                        AdvertiserId = c.Int(),
                        Channel = c.String(),
                        Postcode = c.String(),
                        Context = c.String(),
                        IsFinanceContext = c.Boolean(),
                        DeviceUsed = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tracking",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        AdvertiserSegment = c.String(),
                        BodyType = c.String(),
                        NumberOfSeats = c.Int(),
                        AverageMpg = c.String(),
                        PageName = c.String(),
                        FuelType = c.String(),
                        Userid = c.String(),
                        LocOne = c.String(),
                        DealerId = c.Int(),
                        Platform = c.String(),
                        GreatValue = c.String(),
                        VehicleCheckId = c.String(),
                        EngineSize = c.String(),
                        Co2Emissions = c.Int(),
                        LoggedInStatus = c.String(),
                        FinanceViewSelected = c.Boolean(),
                        DealerProducts = c.String(),
                        AnnualTax = c.Int(),
                        Mileage = c.Int(),
                        VehicleYear = c.Int(),
                        PageNameGranular = c.String(),
                        PageNumber = c.Int(),
                        VehiclePrice = c.Int(),
                        ManufacturerApproved = c.Boolean(),
                        TopSpeed = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        Acceleration = c.String(),
                        Gearbox = c.String(),
                        AdId = c.String(),
                        VehicleCheckStatus = c.String(),
                        NumberOfDoors = c.Int(),
                        ExperimentVariant = c.String(),
                        Section = c.String(),
                        LocationArea = c.String(),
                        MonthlyVehiclePrice = c.String(),
                        NumberOfPhotos = c.Int(),
                        Aoi = c.String(),
                        FinanceRepresentativeApr = c.String(),
                        AdvertAttributes = c.String(),
                        Channel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Metadatum",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateAdded = c.DateTime(nullable: false),
                        PageDataId = c.Int(nullable: false),
                        Name = c.String(),
                        Content = c.String(),
                    })
                .ForeignKey("dbo.PageData", t => t.PageDataId)
                .Index(t => t.PageDataId);
            
            CreateTable(
                "dbo.Seller",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Name = c.String(),
                        IsTradeSeller = c.Boolean(),
                        Products = c.String(),
                        Longitude = c.String(),
                        Latitude = c.String(),
                        BannerUrl = c.String(),
                        DealerWebsite = c.String(),
                        Distance = c.Int(),
                        EmailAddress = c.String(),
                        PrimaryContactNumber = c.String(),
                        SecondaryContactNumber = c.String(),
                        RatingStars = c.String(),
                        RatingTotalReviews = c.Int(),
                        IsTrustedDealer = c.Boolean(),
                        LocationMapLink = c.String(),
                        DirectionMapLink = c.String(),
                        TownAndDistance = c.String(),
                        ProfileUrl = c.String(),
                        SellerEmailLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicle",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        KeyFactsId = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Condition = c.String(),
                        GenerationId = c.String(),
                        Year = c.Int(),
                        Vrm = c.String(),
                        DerivativeId = c.String(),
                        Tax = c.Int(),
                        Co2Emissions = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KeyFacts", t => t.KeyFactsId)
                .Index(t => t.KeyFactsId);
            
            CreateTable(
                "dbo.KeyFacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        ManufacturedYear = c.String(),
                        BodyType = c.String(),
                        Mileage = c.String(),
                        EngineSize = c.String(),
                        Transmission = c.String(),
                        FuelType = c.String(),
                        Doors = c.String(),
                        Seats = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MotResponse",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Registration = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        FirstUsedDate = c.DateTime(),
                        FuelType = c.String(),
                        PrimaryColour = c.String(),
                        ManufactureYear = c.Int(),
                        DvlaId = c.Int(),
                        MotTestExpiryDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MotTest",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        MotResponseId = c.Int(nullable: false),
                        CompletedDate = c.DateTime(),
                        TestResult = c.String(),
                        ExpiryDate = c.DateTime(),
                        OdometerValue = c.Int(),
                        OdometerUnit = c.String(),
                        MotTestNumber = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MotResponse", t => t.MotResponseId)
                .Index(t => t.MotResponseId);
            
            CreateTable(
                "dbo.RfrAndComment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        MotTestId = c.Int(nullable: false),
                        Text = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MotTest", t => t.MotTestId)
                .Index(t => t.MotTestId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RfrAndComment", "MotTestId", "dbo.MotTest");
            DropForeignKey("dbo.MotTest", "MotResponseId", "dbo.MotResponse");
            DropForeignKey("dbo.ApiArticle", "MotResponseId", "dbo.MotResponse");
            DropForeignKey("dbo.ApiArticle", "AutotraderResponseId", "dbo.AutotraderResponse");
            DropForeignKey("dbo.AutotraderResponse", "VehicleId", "dbo.Vehicle");
            DropForeignKey("dbo.Vehicle", "KeyFactsId", "dbo.KeyFacts");
            DropForeignKey("dbo.AutotraderResponse", "SellerId", "dbo.Seller");
            DropForeignKey("dbo.Metadatum", "PageDataId", "dbo.PageData");
            DropForeignKey("dbo.AutotraderResponse", "PageDataId", "dbo.PageData");
            DropForeignKey("dbo.PageData", "TrackingId", "dbo.Tracking");
            DropForeignKey("dbo.PageData", "OdsId", "dbo.Ods");
            DropForeignKey("dbo.AutotraderResponse", "FinanceId", "dbo.Finance");
            DropForeignKey("dbo.AutotraderResponse", "AdvertId", "dbo.Advert");
            DropForeignKey("dbo.Advert", "SocialMediaLinksId", "dbo.SocialMediaLinks");
            DropForeignKey("dbo.Advert", "InstantMessagingId", "dbo.InstantMessaging");
            DropForeignKey("dbo.InstantMessaging", "PreferencesId", "dbo.Preferences");
            DropForeignKey("dbo.ApiArticle", "ArticleId", "dbo.Article");
            DropIndex("dbo.RfrAndComment", new[] { "MotTestId" });
            DropIndex("dbo.MotTest", new[] { "MotResponseId" });
            DropIndex("dbo.Vehicle", new[] { "KeyFactsId" });
            DropIndex("dbo.Metadatum", new[] { "PageDataId" });
            DropIndex("dbo.PageData", new[] { "TrackingId" });
            DropIndex("dbo.PageData", new[] { "OdsId" });
            DropIndex("dbo.InstantMessaging", new[] { "PreferencesId" });
            DropIndex("dbo.Advert", new[] { "SocialMediaLinksId" });
            DropIndex("dbo.Advert", new[] { "InstantMessagingId" });
            DropIndex("dbo.AutotraderResponse", new[] { "PageDataId" });
            DropIndex("dbo.AutotraderResponse", new[] { "FinanceId" });
            DropIndex("dbo.AutotraderResponse", new[] { "SellerId" });
            DropIndex("dbo.AutotraderResponse", new[] { "AdvertId" });
            DropIndex("dbo.AutotraderResponse", new[] { "VehicleId" });
            DropIndex("dbo.ApiArticle", new[] { "MotResponseId" });
            DropIndex("dbo.ApiArticle", new[] { "AutotraderResponseId" });
            DropIndex("dbo.ApiArticle", new[] { "ArticleId" });
            DropTable("dbo.RfrAndComment");
            DropTable("dbo.MotTest");
            DropTable("dbo.MotResponse");
            DropTable("dbo.KeyFacts");
            DropTable("dbo.Vehicle");
            DropTable("dbo.Seller");
            DropTable("dbo.Metadatum");
            DropTable("dbo.Tracking");
            DropTable("dbo.Ods");
            DropTable("dbo.PageData");
            DropTable("dbo.Finance");
            DropTable("dbo.SocialMediaLinks");
            DropTable("dbo.Preferences");
            DropTable("dbo.InstantMessaging");
            DropTable("dbo.Advert");
            DropTable("dbo.AutotraderResponse");
            DropTable("dbo.ApiArticle");
        }
    }
}
