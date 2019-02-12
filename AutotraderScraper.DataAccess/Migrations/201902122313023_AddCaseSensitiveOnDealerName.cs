namespace AutotraderScraper.DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class AddCaseSensitiveOnDealerName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Dealer", new[] { "Name" });
            AlterColumn("dbo.Dealer", "Name", c => c.String(maxLength: 450,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "CaseSensitive",
                        new AnnotationValues(oldValue: null, newValue: "True")
                    },
                }));
            CreateIndex("dbo.Dealer", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Dealer", new[] { "Name" });
            AlterColumn("dbo.Dealer", "Name", c => c.String(maxLength: 450,
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "CaseSensitive",
                        new AnnotationValues(oldValue: "True", newValue: null)
                    },
                }));
            CreateIndex("dbo.Dealer", "Name", unique: true);
        }
    }
}
