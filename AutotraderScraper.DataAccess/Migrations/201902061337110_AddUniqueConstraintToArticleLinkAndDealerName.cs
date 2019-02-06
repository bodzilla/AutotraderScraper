namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintToArticleLinkAndDealerName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Dealer", "Name", c => c.String(maxLength: 450));
            AlterColumn("dbo.Article", "Link", c => c.String(maxLength: 450));
            CreateIndex("dbo.Dealer", "Name", unique: true);
            CreateIndex("dbo.Article", "Link", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Article", new[] { "Link" });
            DropIndex("dbo.Dealer", new[] { "Name" });
            AlterColumn("dbo.Article", "Link", c => c.String());
            AlterColumn("dbo.Dealer", "Name", c => c.String());
        }
    }
}
