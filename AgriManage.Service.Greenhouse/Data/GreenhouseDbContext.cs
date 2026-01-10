using Microsoft.EntityFrameworkCore;
using AgriManage.Service.Greenhouse.Models; // Modelleri görmesi için şart

namespace AgriManage.Service.Greenhouse.Data
{
    public class GreenhouseDbContext : DbContext
    {
        public GreenhouseDbContext(DbContextOptions<GreenhouseDbContext> options) : base(options)
        {
        }

        public DbSet<Sera> Seralar { get; set; }
        public DbSet<Hasat> Hasatlar { get; set; }
        public DbSet<Ekim> Ekimler { get; set; }
    }
}