using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using AutotraderScraper.DataAccess.Migrations;
using AutotraderScraper.Model;
using AutotraderScraper.Model.Api;
using AutotraderScraper.Model.Api.Autotrader;
using AutotraderScraper.Model.Api.Mot;
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

            modelBuilder.Entity<ApiArticle>().ToTable("ApiArticle");

            modelBuilder.Entity<AutotraderResponse>().ToTable("AutotraderResponse");
            modelBuilder.Entity<Advert>().ToTable("Advert");
            modelBuilder.Entity<InstantMessaging>().ToTable("InstantMessaging");
            modelBuilder.Entity<Preferences>().ToTable("Preferences");
            modelBuilder.Entity<SocialMediaLinks>().ToTable("SocialMediaLinks");
            modelBuilder.Entity<Finance>().ToTable("Finance");
            modelBuilder.Entity<PageData>().ToTable("PageData");
            modelBuilder.Entity<Metadatum>().ToTable("Metadatum");
            modelBuilder.Entity<Ods>().ToTable("Ods");
            modelBuilder.Entity<Tracking>().ToTable("Tracking");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<KeyFacts>().ToTable("KeyFacts");

            modelBuilder.Entity<MotResponse>().ToTable("MotResponse");
            modelBuilder.Entity<MotTest>().ToTable("MotTest");
            modelBuilder.Entity<RfrAndComment>().ToTable("RfrAndComment");
        }
    }
}
