

using System.Text.Json.Serialization;

namespace ASP.NET_HomeWork.Models
{
    public class Product : BaseModel
    {
        public int? Cost { get; set; }
        public int? CategoryID { get; set; }
        [JsonIgnore]
        public virtual Category? ProductGroup { get; set; }
        [JsonIgnore]
        public virtual List<ProductStorage>? ProductStorages { get; set; } = [];
    }
}
