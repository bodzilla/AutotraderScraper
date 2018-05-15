namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUpdatesToArticleVersion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleVersion", "Updates", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleVersion", "Updates");
        }
    }
}
