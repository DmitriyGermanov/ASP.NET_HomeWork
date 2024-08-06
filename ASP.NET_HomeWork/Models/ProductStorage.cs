using System.Text.Json.Serialization;

namespace ASP.NET_HomeWork.Models
{
    public class ProductStorage
    {
        public int? ProductId { get; set; }
        public int? StorageID { get; set; }
        [JsonIgnore]
        public virtual Product? Product { get; set; }
        [JsonIgnore]
        public virtual Storage? Storage { get; set; }
    }
}
