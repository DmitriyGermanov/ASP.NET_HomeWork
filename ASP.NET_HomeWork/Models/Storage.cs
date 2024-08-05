namespace ASP.NET_HomeWork.Models
{
    public class Storage : BaseModel
    {
        public virtual List<ProductStorage>? ProductStorages { get; set; } = [];
    }
}
