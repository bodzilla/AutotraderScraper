namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApiArticleToApiArticleVersion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApiArticle", "ArticleId", "dbo.Article");
            DropIndex("dbo.ApiArticle", new[] { "ArticleId" });
            AddColumn("dbo.ApiArticle", "ArticleVersionId", c => c.Int(nullable: false));
            CreateIndex("dbo.ApiArticle", "ArticleVersionId");
            AddForeignKey("dbo.ApiArticle", "ArticleVersionId", "dbo.ArticleVersion", "Id");
            DropColumn("dbo.ApiArticle", "ArticleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApiArticle", "ArticleId", c => c.Int(nullable: false));
            DropForeignKey("dbo.ApiArticle", "ArticleVersionId", "dbo.ArticleVersion");
            DropIndex("dbo.ApiArticle", new[] { "ArticleVersionId" });
            DropColumn("dbo.ApiArticle", "ArticleVersionId");
            CreateIndex("dbo.ApiArticle", "ArticleId");
            AddForeignKey("dbo.ApiArticle", "ArticleId", "dbo.Article", "Id");
        }
    }
}
