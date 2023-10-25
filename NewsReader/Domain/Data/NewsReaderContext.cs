using Microsoft.EntityFrameworkCore;
using NewsReader.Domain.Models;

#nullable disable

namespace NewsReader.Domain.Data
{
    public partial class NewsReaderContext : DbContext
    {
        public NewsReaderContext()
        {
            
        }

        public NewsReaderContext(DbContextOptions<NewsReaderContext> option)
            : base(option)
        {
            
        }

        public virtual DbSet<Keyword> Keywords { get; set; }
        public virtual DbSet<News> Newses { get; set; }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<NewsKeyword> NewsKeywords { get; set; }
        public virtual DbSet<Website> Websites { get; set; }
        public virtual DbSet<WebsiteCategory> WebsiteCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NewsReaderDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Subject).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Publisher)
                    .HasMaxLength(200);
                entity.Property(e => e.Creator)
                    .HasMaxLength(100);
                entity.Property(e => e.Date).IsRequired()
                    .IsUnicode(false)
                    .IsFixedLength(true);
                entity.Property(e => e.Link).IsRequired()
                    .HasMaxLength(250);
                entity.Property(e => e.Media)
                    .HasMaxLength(200);
                entity.Property(e => e.NewsGuid).IsRequired();
                entity.HasOne(e => e.FKWebsite)
                    .WithMany(m => m.Newses)
                    .HasForeignKey(f => f.FKWebsiteId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(e => e.FKCategory)
                    .WithMany(m => m.Newses)
                    .HasForeignKey(f => f.FKCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NewsCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<NewsKeyword>(entity =>
            {
                entity.HasKey(e => e.Id);
                //entity.HasOne(e => e.FKKeyword)
                //    .WithMany(m => m.NewsKeywords)
                //    .HasForeignKey(f => f.FKKeywordId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
                //entity.HasOne(e => e.FKNews)
                //    .WithMany(m => m.NewsKeywords)
                //    .HasForeignKey(f => f.FKNewsId)
                //    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Url).IsRequired()
                    .HasMaxLength(200);
                entity.Property(e=>e.FeedLink).IsRequired()
                    .HasMaxLength(200);
                entity.HasOne(e => e.FKCategory)
                    .WithMany(m => m.Websites)
                    .HasForeignKey(f => f.FKCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WebsiteCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
