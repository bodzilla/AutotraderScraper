namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagLineToArticle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "TagLine", c => c.String());
            DropColumn("dbo.ArticleVersion", "TagLine");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArticleVersion", "TagLine", c => c.String());
            DropColumn("dbo.Article", "TagLine");
        }
    }
}
