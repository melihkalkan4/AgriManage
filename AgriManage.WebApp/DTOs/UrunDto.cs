namespace AgriManage.WebApp.DTOs
{
    public class UrunDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int TarlaId { get; set; }

        // 👇 BURASI KRİTİK: Soru işareti (?) ekle.
        // Böylece form validasyonu "Bu alan boş" diye bağırmaz.
        public string? OwnerId { get; set; }
    }
}