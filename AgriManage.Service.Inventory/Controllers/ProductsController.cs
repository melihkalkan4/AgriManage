using Microsoft.AspNetCore.Mvc;
using AgriManage.Service.Inventory.Data;
using AgriManage.Service.Inventory.Models;
using System.Linq;

namespace AgriManage.Service.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        // Dependency Injection: Veritabanı bağlantısını içeri alıyoruz
        public ProductsController(InventoryDbContext context)
        {
            _context = context;
        }

        // TÜM ÜRÜNLERİ GETİR
        // GET: api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        // YENİ ÜRÜN EKLE (Veritabanını doldurmak için gerekli)
        // POST: api/products
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges(); // SQL'e kaydet
            return Ok(product);
        }

        // KULLANICIYA AİT ÜRÜNLERİ GETİR
        // GET: api/products/user/{userId}
        [HttpGet("user/{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var userProducts = _context.Products.Where(p => p.OwnerId == userId).ToList();
            return Ok(userProducts);
        }
    }
}