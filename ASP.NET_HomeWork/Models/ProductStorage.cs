namespace ASP.NET_HomeWork.Models
{
    public class ProductStorage
    {
        public int? ProductId { get; set; }
        public int? StorageID { get; set; }
        public virtual Product? Product { get; set; }
        public virtual Storage? Storage { get; set; }
    }
}
