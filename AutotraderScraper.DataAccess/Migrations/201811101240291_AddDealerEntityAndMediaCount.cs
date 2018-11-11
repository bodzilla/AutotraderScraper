namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDealerEntityAndMediaCount : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dealer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Name = c.String(),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Article", "MediaCount", c => c.Int(nullable: false));
            AddColumn("dbo.Article", "DealerId", c => c.Int());
            CreateIndex("dbo.Article", "DealerId");
            AddForeignKey("dbo.Article", "DealerId", "dbo.Dealer", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Article", "DealerId", "dbo.Dealer");
            DropIndex("dbo.Article", new[] { "DealerId" });
            DropColumn("dbo.Article", "DealerId");
            DropColumn("dbo.Article", "MediaCount");
            DropTable("dbo.Dealer");
        }
    }
}
