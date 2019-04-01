namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeApiForeignKeysNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AutotraderResponse", new[] { "VehicleId" });
            DropIndex("dbo.AutotraderResponse", new[] { "AdvertId" });
            DropIndex("dbo.AutotraderResponse", new[] { "SellerId" });
            DropIndex("dbo.AutotraderResponse", new[] { "FinanceId" });
            DropIndex("dbo.AutotraderResponse", new[] { "PageDataId" });
            DropIndex("dbo.Advert", new[] { "InstantMessagingId" });
            DropIndex("dbo.Advert", new[] { "SocialMediaLinksId" });
            DropIndex("dbo.InstantMessaging", new[] { "PreferencesId" });
            DropIndex("dbo.ImageUrls", new[] { "AdvertId" });
            DropIndex("dbo.PageData", new[] { "OdsId" });
            DropIndex("dbo.PageData", new[] { "TrackingId" });
            DropIndex("dbo.Metadatum", new[] { "PageDataId" });
            DropIndex("dbo.Vehicle", new[] { "KeyFactsId" });
            DropIndex("dbo.MotTest", new[] { "MotResponseId" });
            DropIndex("dbo.RfrAndComment", new[] { "MotTestId" });
            AlterColumn("dbo.AutotraderResponse", "VehicleId", c => c.Int());
            AlterColumn("dbo.AutotraderResponse", "AdvertId", c => c.Int());
            AlterColumn("dbo.AutotraderResponse", "SellerId", c => c.Int());
            AlterColumn("dbo.AutotraderResponse", "FinanceId", c => c.Int());
            AlterColumn("dbo.AutotraderResponse", "PageDataId", c => c.Int());
            AlterColumn("dbo.Advert", "InstantMessagingId", c => c.Int());
            AlterColumn("dbo.Advert", "SocialMediaLinksId", c => c.Int());
            AlterColumn("dbo.InstantMessaging", "PreferencesId", c => c.Int());
            AlterColumn("dbo.ImageUrls", "AdvertId", c => c.Int());
            AlterColumn("dbo.PageData", "OdsId", c => c.Int());
            AlterColumn("dbo.PageData", "TrackingId", c => c.Int());
            AlterColumn("dbo.Metadatum", "PageDataId", c => c.Int());
            AlterColumn("dbo.Vehicle", "KeyFactsId", c => c.Int());
            AlterColumn("dbo.MotTest", "MotResponseId", c => c.Int());
            AlterColumn("dbo.RfrAndComment", "MotTestId", c => c.Int());
            CreateIndex("dbo.AutotraderResponse", "VehicleId");
            CreateIndex("dbo.AutotraderResponse", "AdvertId");
            CreateIndex("dbo.AutotraderResponse", "SellerId");
            CreateIndex("dbo.AutotraderResponse", "FinanceId");
            CreateIndex("dbo.AutotraderResponse", "PageDataId");
            CreateIndex("dbo.Advert", "InstantMessagingId");
            CreateIndex("dbo.Advert", "SocialMediaLinksId");
            CreateIndex("dbo.InstantMessaging", "PreferencesId");
            CreateIndex("dbo.ImageUrls", "AdvertId");
            CreateIndex("dbo.PageData", "OdsId");
            CreateIndex("dbo.PageData", "TrackingId");
            CreateIndex("dbo.Metadatum", "PageDataId");
            CreateIndex("dbo.Vehicle", "KeyFactsId");
            CreateIndex("dbo.MotTest", "MotResponseId");
            CreateIndex("dbo.RfrAndComment", "MotTestId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RfrAndComment", new[] { "MotTestId" });
            DropIndex("dbo.MotTest", new[] { "MotResponseId" });
            DropIndex("dbo.Vehicle", new[] { "KeyFactsId" });
            DropIndex("dbo.Metadatum", new[] { "PageDataId" });
            DropIndex("dbo.PageData", new[] { "TrackingId" });
            DropIndex("dbo.PageData", new[] { "OdsId" });
            DropIndex("dbo.ImageUrls", new[] { "AdvertId" });
            DropIndex("dbo.InstantMessaging", new[] { "PreferencesId" });
            DropIndex("dbo.Advert", new[] { "SocialMediaLinksId" });
            DropIndex("dbo.Advert", new[] { "InstantMessagingId" });
            DropIndex("dbo.AutotraderResponse", new[] { "PageDataId" });
            DropIndex("dbo.AutotraderResponse", new[] { "FinanceId" });
            DropIndex("dbo.AutotraderResponse", new[] { "SellerId" });
            DropIndex("dbo.AutotraderResponse", new[] { "AdvertId" });
            DropIndex("dbo.AutotraderResponse", new[] { "VehicleId" });
            AlterColumn("dbo.RfrAndComment", "MotTestId", c => c.Int(nullable: false));
            AlterColumn("dbo.MotTest", "MotResponseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Vehicle", "KeyFactsId", c => c.Int(nullable: false));
            AlterColumn("dbo.Metadatum", "PageDataId", c => c.Int(nullable: false));
            AlterColumn("dbo.PageData", "TrackingId", c => c.Int(nullable: false));
            AlterColumn("dbo.PageData", "OdsId", c => c.Int(nullable: false));
            AlterColumn("dbo.ImageUrls", "AdvertId", c => c.Int(nullable: false));
            AlterColumn("dbo.InstantMessaging", "PreferencesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Advert", "SocialMediaLinksId", c => c.Int(nullable: false));
            AlterColumn("dbo.Advert", "InstantMessagingId", c => c.Int(nullable: false));
            AlterColumn("dbo.AutotraderResponse", "PageDataId", c => c.Int(nullable: false));
            AlterColumn("dbo.AutotraderResponse", "FinanceId", c => c.Int(nullable: false));
            AlterColumn("dbo.AutotraderResponse", "SellerId", c => c.Int(nullable: false));
            AlterColumn("dbo.AutotraderResponse", "AdvertId", c => c.Int(nullable: false));
            AlterColumn("dbo.AutotraderResponse", "VehicleId", c => c.Int(nullable: false));
            CreateIndex("dbo.RfrAndComment", "MotTestId");
            CreateIndex("dbo.MotTest", "MotResponseId");
            CreateIndex("dbo.Vehicle", "KeyFactsId");
            CreateIndex("dbo.Metadatum", "PageDataId");
            CreateIndex("dbo.PageData", "TrackingId");
            CreateIndex("dbo.PageData", "OdsId");
            CreateIndex("dbo.ImageUrls", "AdvertId");
            CreateIndex("dbo.InstantMessaging", "PreferencesId");
            CreateIndex("dbo.Advert", "SocialMediaLinksId");
            CreateIndex("dbo.Advert", "InstantMessagingId");
            CreateIndex("dbo.AutotraderResponse", "PageDataId");
            CreateIndex("dbo.AutotraderResponse", "FinanceId");
            CreateIndex("dbo.AutotraderResponse", "SellerId");
            CreateIndex("dbo.AutotraderResponse", "AdvertId");
            CreateIndex("dbo.AutotraderResponse", "VehicleId");
        }
    }
}
