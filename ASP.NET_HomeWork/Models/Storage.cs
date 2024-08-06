
using System.Text.Json.Serialization;

namespace ASP.NET_HomeWork.Models
{
    public class Storage : BaseModel
    {
        [JsonIgnore]
        public virtual List<ProductStorage>? ProductStorages { get; set; } = [];
    }
}
