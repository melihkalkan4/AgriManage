using Microsoft.EntityFrameworkCore;
using AgriManage.Service.Inventory.Models;

namespace AgriManage.Service.Inventory.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}