using System.ComponentModel.DataAnnotations;

namespace AgriManage.DataAccess.Models
{
    public class Departman
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Ad { get; set; }

        public virtual ICollection<Pozisyon> Pozisyonlar { get; set; }
    }
}
