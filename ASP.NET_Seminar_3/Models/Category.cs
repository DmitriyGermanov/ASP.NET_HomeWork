using System.Text.Json.Serialization;

namespace ASP.NET_Seminar3.Models
{
    public class Category : BaseModel
    {
        [JsonIgnore]
        public virtual List<Product> Products { get; set; } = [];
    }
}
