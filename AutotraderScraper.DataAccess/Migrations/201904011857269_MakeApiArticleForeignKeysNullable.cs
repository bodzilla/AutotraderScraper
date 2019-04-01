namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeApiArticleForeignKeysNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApiArticle", new[] { "AutotraderResponseId" });
            DropIndex("dbo.ApiArticle", new[] { "MotResponseId" });
            AlterColumn("dbo.ApiArticle", "AutotraderResponseId", c => c.Int());
            AlterColumn("dbo.ApiArticle", "MotResponseId", c => c.Int());
            CreateIndex("dbo.ApiArticle", "AutotraderResponseId");
            CreateIndex("dbo.ApiArticle", "MotResponseId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApiArticle", new[] { "MotResponseId" });
            DropIndex("dbo.ApiArticle", new[] { "AutotraderResponseId" });
            AlterColumn("dbo.ApiArticle", "MotResponseId", c => c.Int(nullable: false));
            AlterColumn("dbo.ApiArticle", "AutotraderResponseId", c => c.Int(nullable: false));
            CreateIndex("dbo.ApiArticle", "MotResponseId");
            CreateIndex("dbo.ApiArticle", "AutotraderResponseId");
        }
    }
}
