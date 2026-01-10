namespace AgriManage.Service.Greenhouse.Models
{
    public class Sera
    {
        public int Id { get; set; }
        public string Ad { get; set; } = string.Empty; // Null hatasını çözer
        public string Konum { get; set; } = string.Empty;
        public int Buyukluk { get; set; }
        public string Tip { get; set; } = "Plastik";
        public bool IsitmaVarMi { get; set; }
        public string OwnerId { get; set; } = string.Empty;
    }
}