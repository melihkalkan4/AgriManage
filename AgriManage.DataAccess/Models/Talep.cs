using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriManage.DataAccess.Models
{
    public class Talep
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Baslik { get; set; } // Örn: Yıllık İzin, Gübre Alımı

        public string Aciklama { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        // Talep Durumu: 0: Bekliyor, 1: Onaylandı, 2: Reddedildi
        public int DurumId { get; set; } = 0;

        // Talebi kim yaptı?
        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }

        // Talep Tipi (Opsiyonel): "Satın Alma", "İzin", "Arıza"
        public string Tur { get; set; }
    }
}