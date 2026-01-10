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

        // ---------------------------------------------------------
        // 1. LİSTELEME: STOKU OLANLARI GETİR
        // ---------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll([FromQuery] int? tarlaId)
        {
            // Eğer TarlaId 0 veya null ise -> Depodaki ürünleri getir
            if (tarlaId == 0 || tarlaId == null)
            {
                // && p.StockQuantity > 0 : Biten ürünleri gizle
                return Ok(_context.Products.Where(p => p.TarlaId == 0 && p.StockQuantity > 0).ToList());
            }

            // Belirli bir tarlanın ürünleri
            var products = _context.Products.Where(p => p.TarlaId == tarlaId.Value && p.StockQuantity > 0).ToList();
            return Ok(products);
        }

        // ---------------------------------------------------------
        // 2. YENİ ÜRÜN EKLEME (SATIN ALMA)
        // ---------------------------------------------------------
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Token'dan ID çek
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                         ?? User.FindFirst("UserId")?.Value;

            product.OwnerId = userId ?? "System_Unknown";

            // Eğer TarlaId gelmediyse 0 (Depo) kabul et.
            if (product.TarlaId < 0) product.TarlaId = 0;

            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok(product);
        }

        // ---------------------------------------------------------
        // 3. TRANSFER: DEPODAN TARLAYA SEVK ET 🚚
        // ---------------------------------------------------------
        [HttpPost("transfer")]
        public IActionResult TransferProduct([FromQuery] int productId, [FromQuery] int targetTarlaId, [FromQuery] int quantity)
        {
            // 1. Kaynak Ürünü Bul
            var sourceProduct = _context.Products.Find(productId);
            if (sourceProduct == null) return NotFound("Ürün bulunamadı.");

            // 2. Stok Yeterli mi?
            if (sourceProduct.StockQuantity < quantity)
            {
                return BadRequest($"Yetersiz Stok! Mevcut: {sourceProduct.StockQuantity} adet.");
            }

            // 3. Depodan Düş
            sourceProduct.StockQuantity -= quantity;

            // 4. Tarlaya Yeni Kayıt Olarak Ekle
            // (Eğer iade yapılıyorsa targetTarlaId=0 gelir ve depoya geri eklenir)
            var newProductForField = new Product
            {
                Name = sourceProduct.Name,
                Price = sourceProduct.Price,
                StockQuantity = quantity,
                TarlaId = targetTarlaId,
                OwnerId = sourceProduct.OwnerId
            };

            _context.Products.Add(newProductForField);
            _context.SaveChanges();

            return Ok(new { message = "Transfer başarılı", sourceStock = sourceProduct.StockQuantity });
        }

        // ---------------------------------------------------------
        // 4. TÜKETİM / SARFİYAT: ÜRÜNÜ KULLAN VE YOK ET 📉 (YENİ)
        // ---------------------------------------------------------
        [HttpPost("consume")]
        public IActionResult ConsumeProduct([FromQuery] int productId, [FromQuery] int quantity)
        {
            // 1. Ürünü Bul
            var product = _context.Products.Find(productId);
            if (product == null) return NotFound("Ürün bulunamadı.");

            // 2. Stok Kontrolü
            if (product.StockQuantity < quantity)
            {
                return BadRequest($"Yetersiz Stok! Mevcut: {product.StockQuantity}");
            }

            // 3. Stoktan Düş (Yok et)
            product.StockQuantity -= quantity;

            // 4. Kaydet
            _context.SaveChanges();
            return Ok(new { message = "Tüketim başarılı", remainingStock = product.StockQuantity });
        }
    }
}