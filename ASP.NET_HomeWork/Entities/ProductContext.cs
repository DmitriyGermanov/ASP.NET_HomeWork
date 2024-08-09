using ASP.NET_HomeWork.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_HomeWork.Entities
{
    public class ProductContext : DbContext
    {

        public DbSet<Product>? Products { get; set; }
        public DbSet<ProductStorage>? ProductStorages { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Storage>? Storages { get; set; }

        public ProductContext()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Database=ProductsBD;Uid=root;Pwd=;")
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

                entity.HasMany(p => p.ProductStorages)
                      .WithOne(ps => ps.Product)
                      .HasForeignKey(ps => ps.ProductId);
            });

            modelBuilder.Entity<ProductStorage>(entity =>
            {
                entity.HasKey(ps => new { ps.ProductId, ps.StorageID })
                      .HasName("PK_ProductStorage");

                entity.HasOne(ps => ps.Product)
                      .WithMany(p => p.ProductStorages)
                      .HasForeignKey(ps => ps.ProductId);

                entity.HasOne(ps => ps.Storage)
                      .WithMany(s => s.ProductStorages)
                      .HasForeignKey(ps => ps.StorageID);
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.HasKey(s => s.Id)
                      .HasName("PK_StorageID");
                entity.HasIndex(s => s.Id)
                      .IsUnique();
                entity.ToTable("Storages");

                entity.HasMany(s => s.ProductStorages)
                      .WithOne(ps => ps.Storage)
                      .HasForeignKey(ps => ps.StorageID);
            });
        }
    }
}