using ASP.NET_Seminar3.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Seminar_3
{
    public class Seminar3Context(string connectionString) : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Storage> Storages { get; set; }
        private readonly string? _connectionString = connectionString;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString ?? throw new NullReferenceException("Connection String can't be Null."))
                          .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id)
                      .HasName("PK_CategoryID");
                entity.HasIndex(c => c.Id)
                      .IsUnique();
                entity.ToTable("Categories");

                entity.HasMany(c => c.Products)
                      .WithOne(p => p.ProductGroup)
                      .HasForeignKey(p => p.CategoryID);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id)
                      .HasName("PK_ProductID");
                entity.HasIndex(p => p.Id)
                      .IsUnique();
                entity.ToTable("Products");

                entity.HasOne(p => p.ProductGroup)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryID);
            });


            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                      .HasName("PK_StorageID");
                entity.HasIndex(s => s.Id)
                      .IsUnique();
                entity.ToTable("Storages");
            });
        }
    }
}