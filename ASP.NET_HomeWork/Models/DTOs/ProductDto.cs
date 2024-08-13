namespace ASP.NET_HomeWork.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? Cost { get; set; }
        public int? CategoryID { get; set; }

        public override string? ToString() =>
            $"Product ID = {Id},\n" +
            $"Product Name = {Name},\n" +
            $"Product Description = {Description},\n" +
            $"Product Cost = {Cost},\n" +
            $"Product CategoryID = {Id}";
    }
}
