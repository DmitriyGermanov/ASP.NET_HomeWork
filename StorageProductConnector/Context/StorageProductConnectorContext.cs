using Microsoft.EntityFrameworkCore;

namespace StorageProductConnector.Context
{
    public class StorageProductConnectorContext(string connectionString) : DbContext
    {
        private readonly string _connectionString = connectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString ?? throw new NullReferenceException("Connection String can't be Null."))
                          .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductStorage>(sp =>
            {
                sp.HasKey(sp => sp.Id).HasName("PK_StorageProductConnector");
            });
        }
    }
}
