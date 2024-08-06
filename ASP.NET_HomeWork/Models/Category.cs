using Newtonsoft.Json;

namespace ASP.NET_HomeWork.Models
{
    public class Category : BaseModel
    {
        [JsonIgnore]
        public virtual List<Product> Products { get; set; } = [];
    }
}
