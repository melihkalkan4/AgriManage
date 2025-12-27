using Microsoft.AspNetCore.Mvc;
using AgriManage.Service.Inventory.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.Service.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Geçici Veritabanımız (RAM üzerinde)
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Domates Tohumu", StockQuantity = 100, Price = 50, OwnerId = "user_ciftci" },
            new Product { Id = 2, Name = "Sıvı Gübre", StockQuantity = 20, Price = 450, OwnerId = "user_ciftci" },
            new Product { Id = 3, Name = "Traktör Tekeri", StockQuantity = 4, Price = 5000, OwnerId = "admin" }
        };

        // TÜM ÜRÜNLERİ GETİR (Admin için)
        // Adres: GET api/products
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_products);
        }

        // KULLANICIYA GÖRE GETİR (Çiftçiler için)
        // Adres: GET api/products/my-products/user_ciftci
        [HttpGet("my-products/{userId}")]
        public IActionResult GetByUserId(string userId)
        {
            var userProducts = _products.Where(p => p.OwnerId == userId).ToList();

            if (!userProducts.Any())
                return NotFound("Bu kullanıcıya ait ürün bulunamadı.");

            return Ok(userProducts);
        }
    }
}