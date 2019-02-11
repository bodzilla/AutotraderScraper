namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateEndedToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "DateEnded", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Article", "DateEnded");
        }
    }
}
