using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.BusinessLogic.Services
{
    public class TarlaService : ITarlaService
    {
        private readonly ApplicationDbContext _context;

        public TarlaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tarla> GetAllTarlalar(string userId)
        {
            return _context.Tarlalar
                .Include(t => t.Lokasyon)
                // BU SATIR ÇOK ÖNEMLİ: Tarlanın dolu görünmesini sağlar
                .Include(t => t.EkimPlanlari).ThenInclude(ep => ep.Urun)
                // BU SATIR ÇOK ÖNEMLİ: Listede eksik tarlaların görünmesini sağlar
                .Where(t => t.ApplicationUserId == userId || t.ApplicationUserId == null)
                .ToList();
        }

        public Tarla GetTarlaById(int id)
        {
            return _context.Tarlalar
                .Include(t => t.Lokasyon)
                .FirstOrDefault(t => t.Id == id);
        }

        public void CreateTarla(Tarla tarla)
        {
            _context.Tarlalar.Add(tarla);
            _context.SaveChanges();
        }

        public void UpdateTarla(Tarla tarla)
        {
            _context.Tarlalar.Update(tarla);
            _context.SaveChanges();
        }

        public void DeleteTarla(int id)
        {
            var tarla = _context.Tarlalar.Find(id);
            if (tarla != null)
            {
                _context.Tarlalar.Remove(tarla);
                _context.SaveChanges();
            }
        }

        public List<Lokasyon> GetAllLokasyonlar()
        {
            return _context.Lokasyonlar.ToList();
        }

        public List<Urun> GetAllUrunler()
        {
            return _context.Urunler.ToList();
        }

        public List<Sezon> GetAktifSezonlar()
        {
            return _context.Sezonlar.Where(s => s.Aktif).ToList();
        }

        public void CreateEkimPlani(EkimPlani ekimPlani)
        {
            // Mükerrer Kayıt Önlemi: Eğer tarlada zaten aktif bir ekim varsa yenisini ekleme
            var aktifVarMi = _context.EkimPlanlari.Any(e => e.TarlaId == ekimPlani.TarlaId && e.HasatTarihi == null);
            if (!aktifVarMi)
            {
                _context.EkimPlanlari.Add(ekimPlani);
                _context.SaveChanges();
            }
        }

        public List<EkimPlani> GetAktifEkimler(string userId)
        {
            return _context.EkimPlanlari
                .Include(e => e.Tarla)
                .Include(e => e.Urun)
                .Include(e => e.Sezon)
                .Where(e => (e.Tarla.ApplicationUserId == userId || e.Tarla.ApplicationUserId == null)
                            && e.HasatTarihi == null)
                .OrderByDescending(e => e.EkimTarihi)
                .ToList();
        }

        public List<EkimPlani> GetGecmisEkimler(string userId)
        {
            return _context.EkimPlanlari
                .Include(e => e.Tarla)
                .Include(e => e.Urun)
                .Include(e => e.Sezon)
                .Where(e => (e.Tarla.ApplicationUserId == userId || e.Tarla.ApplicationUserId == null)
                            && e.HasatTarihi != null)
                .OrderByDescending(e => e.HasatTarihi)
                .ToList();
        }

        public EkimPlani GetSonEkimByTarlaId(int tarlaId)
        {
            return _context.EkimPlanlari
                .Include(e => e.Urun)
                .Where(e => e.TarlaId == tarlaId && e.HasatTarihi == null)
                .OrderByDescending(e => e.EkimTarihi)
                .FirstOrDefault();
        }

        public void HasatYap(int ekimPlaniId, decimal gerceklesenVerim, DateTime hasatTarihi)
        {
            var plan = _context.EkimPlanlari.Find(ekimPlaniId);
            if (plan != null)
            {
                plan.HasatTarihi = hasatTarihi;
                plan.GerceklesenVerimKg = gerceklesenVerim;
                _context.SaveChanges();
            }
        }
    }
}