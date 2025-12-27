namespace AgriManage.WebApp.DTOs
{
    public class UrunDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string OwnerId { get; set; }
    }
}