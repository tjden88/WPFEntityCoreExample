using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WPFEntityCoreExample.Entityes;

namespace WPFEntityCoreExample
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<CatalogTrueView> CatalogTrueViews { get; set; }

        public Context()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=wpfENtityTestDB;Trusted_Connection=True;");
            //optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.LogTo(message => Debug.WriteLine(message), Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // FlueNet API there
        {
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "StartData", Description = "StartDescript" }); // Начальные данные


            modelBuilder.Entity<CatalogTrueView>((tw =>
            {
                tw.HasNoKey();
                tw.ToView("CatalogView");
            }));
        }
    }
}
