namespace AgriManage.Service.Inventory.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }        // Ürün Adı
        public int StockQuantity { get; set; }  // Stok Miktarı
        public decimal Price { get; set; }      // Fiyat
        public string OwnerId { get; set; }     // Bu ürün kime ait?
    }
}