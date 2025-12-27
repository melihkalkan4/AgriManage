using Microsoft.AspNetCore.Mvc;
using AgriManage.Service.Inventory.Data;
using AgriManage.Service.Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace AgriManage.Service.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductsController(InventoryDbContext context)
        {
            _context = context;
        }

        // 1. LİSTELEME (Mevcut Mantık)
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? tarlaId)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            // Eğer TarlaId 0 veya null ise -> Depodaki (Boşta) ürünleri getir
            if (tarlaId == 0 || tarlaId == null)
            {
                // TarlaId'si 0 olanlar "Depo"da demektir.
                return Ok(_context.Products.Where(p => p.TarlaId == 0).ToList());
            }

            // Belirli bir tarlanın ürünleri
            var products = _context.Products.Where(p => p.TarlaId == tarlaId.Value).ToList();
            return Ok(products);
        }

        // 2. YENİ ÜRÜN EKLEME (Sorunu Çözen Kısım)
        // Artık OwnerId'yi Token'dan alıyoruz ve TarlaId zorunlu değil (0 olabilir)
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // A) TOKEN'DAN ID ÇEKME 🕵️‍♂️
            // TokenService'in içine koyduğumuz ID'yi buradan okuyoruz.
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("UserId")?.Value;

            // Eğer bir şekilde ID bulunamazsa "Bilinmeyen" yazsın ama patlamasın.
            product.OwnerId = userId ?? "System_Unknown";

            // B) DEPOYA EKLEME MANTIĞI
            // Eğer TarlaId gelmediyse 0 (Depo) kabul et.
            if (product.TarlaId < 0) product.TarlaId = 0;

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        // 3. YENİ ÖZELLİK: ÜRÜN TRANSFERİ (Zimmetleme) 🚚
        // Bir ürünü alıp başka bir tarlaya atar (Depodan Tarlaya veya Tarladan Tarlaya)
        [HttpPut("assign")]
        public IActionResult AssignProduct([FromQuery] int productId, [FromQuery] int targetTarlaId)
        {
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound("Ürün bulunamadı.");

            // Ürünün konumunu değiştiriyoruz
            product.TarlaId = targetTarlaId;

            _context.SaveChanges();
            return Ok(new { message = "Transfer başarılı", product });
        }
    }
}