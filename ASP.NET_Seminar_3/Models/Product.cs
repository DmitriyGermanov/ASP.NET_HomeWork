using System.Text.Json.Serialization;

namespace ASP.NET_Seminar3.Models
{
    public class Product : BaseModel
    {
        public int? Cost { get; set; }
        public int? CategoryID { get; set; }
        [JsonIgnore]
        public virtual Category? ProductGroup { get; set; }
    }
}
