using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// 👇 BU İKİ SATIR OLMADAN HATA VERİR!
using AgriManage.Service.Greenhouse.Data;
using AgriManage.Service.Greenhouse.Models;

namespace AgriManage.Service.Greenhouse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeraController : ControllerBase
    {
        private readonly GreenhouseDbContext _context;

        public SeraController(GreenhouseDbContext context)
        {
            _context = context;
        }

        // --- GET ALL ---
        [HttpGet]
        public IActionResult GetAll()
        {
            if (_context.Seralar == null) return Ok(new List<Sera>());
            return Ok(_context.Seralar.ToList());
        }

        // --- GET BY ID ---
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sera = _context.Seralar.Find(id);
            if (sera == null) return NotFound();
            return Ok(sera);
        }

        // --- CREATE ---
        [HttpPost]
        public IActionResult Create(Sera sera)
        {
            // Null hatasını önlemek için kontrol
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "TestUser";
            sera.OwnerId = userId;

            _context.Seralar.Add(sera);
            _context.SaveChanges();
            return Ok(sera);
        }

        // --- UPDATE ---
        [HttpPut]
        public IActionResult Update(Sera sera)
        {
            _context.Seralar.Update(sera);
            _context.SaveChanges();
            return Ok(sera);
        }

        // --- DELETE ---
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sera = _context.Seralar.Find(id);
            if (sera == null) return NotFound();
            _context.Seralar.Remove(sera);
            _context.SaveChanges();
            return Ok();
        }

        // --- HASAT EKLE ---
        // POST: api/sera/hasat
        [HttpPost("hasat")]
        public IActionResult AddHasat(Hasat hasat, [FromQuery] bool kokSokumu = false)
        {
            // 1. Hasadı Kaydet
            hasat.Tarih = DateTime.Now;
            _context.Hasatlar.Add(hasat);

            // 2. Eğer "Kök Sökümü" işaretlendiyse, ilgili ekimi bul ve bitir
            if (kokSokumu && hasat.EkimId.HasValue)
            {
                var ekim = _context.Ekimler.Find(hasat.EkimId.Value);
                if (ekim != null)
                {
                    ekim.AktifMi = false; // Artık listede gözükmeyecek (Tarihçeye düşecek)
                    _context.Ekimler.Update(ekim);
                }
            }

            _context.SaveChanges();
            return Ok(hasat);
        }

        // --- HASAT GETİR ---
        [HttpGet("hasat/{seraId}")]
        public IActionResult GetHasatlar(int seraId)
        {
            var list = _context.Hasatlar.Where(x => x.SeraId == seraId).OrderByDescending(x => x.Tarih).ToList();
            return Ok(list);
        }

        // --- EKİM EKLE (YENİ) ---
        [HttpPost("ekim")]
        public IActionResult AddEkim(Ekim ekim)
        {
            if (ekim.EkimTarihi == DateTime.MinValue) ekim.EkimTarihi = DateTime.Now;
            ekim.AktifMi = true;
            _context.Ekimler.Add(ekim);
            _context.SaveChanges();
            return Ok(ekim);
        }

        // --- EKİM GETİR (YENİ) ---
        [HttpGet("ekim/{seraId}")]
        public IActionResult GetEkimler(int seraId)
        {
            var list = _context.Ekimler
                               .Where(x => x.SeraId == seraId && x.AktifMi == true)
                               .OrderByDescending(x => x.EkimTarihi)
                               .ToList();
            return Ok(list);
        }
    }
}