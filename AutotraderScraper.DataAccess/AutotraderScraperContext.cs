using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using AutotraderScraper.DataAccess.Migrations;
using AutotraderScraper.Model;

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
            Database.SetInitializer(new CreateDatabaseIfNotExists<AutotraderScraperContext>());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<CarMake>().ToTable("CarMake");
            modelBuilder.Entity<CarModel>().ToTable("CarModel");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<ArticleVersion>().ToTable("ArticleVersion");
        }
    }
}
