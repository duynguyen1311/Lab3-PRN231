namespace ProductManagementAPI.Dto
{
    public class ProductReponse
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? UnitsInStock { get; set; }
    }
    public class ProductAddRequest
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public string? UnitsInStock { get; set; }
    }
}
