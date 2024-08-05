namespace ASP.NET_HomeWork.Models
{
    public class Product : BaseModel
    {
        public int? Cost { get; set; }
        public int? CategoryID { get; set; }
        public virtual Category? ProductGroup { get; set; }
        public virtual List<ProductStorage>? ProductStorages { get; set; } = [];
    }
}
