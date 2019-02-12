using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using AutotraderScraper.DataAccess.Migrations;
using AutotraderScraper.Model;
using AutotraderScraper.Model.Attributes;

namespace AutotraderScraper.DataAccess
{
    public class AutotraderScraperContext : DbContext
    {
        public AutotraderScraperContext()
            : base("AutotraderScraperConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AutotraderScraperContext, Configuration>());

            modelBuilder.Conventions.Add(new AttributeToColumnAnnotationConvention<CaseSensitiveAttribute, bool>("CaseSensitive", (property, attributes) => attributes.Single().IsEnabled));
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<CarMake>().ToTable("CarMake");
            modelBuilder.Entity<CarModel>().ToTable("CarModel");
            modelBuilder.Entity<Dealer>().ToTable("Dealer");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<ArticleVersion>().ToTable("ArticleVersion");
        }
    }
}
