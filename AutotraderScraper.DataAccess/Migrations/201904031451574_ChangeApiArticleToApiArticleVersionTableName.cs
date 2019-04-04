namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApiArticleToApiArticleVersionTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApiArticle", newName: "ApiArticleVersion");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ApiArticleVersion", newName: "ApiArticle");
        }
    }
}
