using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsfeederApi.Models;

namespace NewsfeederApi.Data
{
    public class NewsfeederContext : DbContext
    {
        public NewsfeederContext(DbContextOptions<NewsfeederContext> options) : base(options) { }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Source> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Source)              // En artikel har en källa
                .WithMany(s => s.Articles)          // En källa har många artiklar
                .HasForeignKey(a => a.SourceId);    // Definierar främmande nyckel för SourceId
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)            // En Arikel har en kategori
                .WithMany(c => c.Articles)          // Varje kategori har många artiklar
                .HasForeignKey(a => a.CategoryId);  // FK för Category i artikel
            modelBuilder.Entity<Category>().ToTable("Categories");

        }
    }
}