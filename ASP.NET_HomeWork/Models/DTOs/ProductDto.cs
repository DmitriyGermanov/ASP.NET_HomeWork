namespace ASP.NET_HomeWork.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Cost { get; set; }
        public int? CategoryID { get; set; }
    }
}
