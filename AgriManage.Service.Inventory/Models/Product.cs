namespace AgriManage.Service.Inventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int TarlaId { get; set; }

        // --- GÜNCELLENEN KISIM ---
        // Burayı da nullable yapıyoruz ki API, JSON'da bu alanı göremeyince 400 hatası vermesin.
        public string? OwnerId { get; set; }
    }
}