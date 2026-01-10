using AgriManage.DataAccess.Data;
using AgriManage.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AgriManage.BusinessLogic.Services
{
    public class VardiyaService : IVardiyaService
    {
        private readonly ApplicationDbContext _context;

        public VardiyaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Vardiya> GetVardiyaTanimlari() => _context.Vardiyalar.ToList();

        public void CreateVardiyaTanim(Vardiya vardiya)
        {
            _context.Vardiyalar.Add(vardiya);
            _context.SaveChanges();
        }

        // DÜZELTME 1: Parametre olarak 'DayOfWeek gun' yerine 'DateTime tarih' alıyoruz.
        public void VardiyaAta(int personelId, int vardiyaId, DateTime tarih)
        {
            // Veritabanında aynı gün kayıt var mı kontrol ediyoruz (.Date kullanarak saat farkını yok sayıyoruz)
            var mevcut = _context.PersonelVardiyalari
                .FirstOrDefault(p => p.PersonelId == personelId && p.Tarih.Date == tarih.Date);

            if (mevcut != null)
            {
                // Varsa güncelle
                mevcut.VardiyaId = vardiyaId;
                _context.PersonelVardiyalari.Update(mevcut);
            }
            else
            {
                // Yoksa yeni ekle
                // DÜZELTME 2: 'Gun' yerine 'Tarih' özelliğini kullanıyoruz.
                var atama = new PersonelVardiya
                {
                    PersonelId = personelId,
                    VardiyaId = vardiyaId,
                    Tarih = tarih
                };
                _context.PersonelVardiyalari.Add(atama);
            }
            _context.SaveChanges();
        }

        public List<PersonelVardiya> TumProgramiGetir()
        {
            return _context.PersonelVardiyalari
                .Include(pv => pv.Vardiya)
                .Include(pv => pv.Personel)
                    .ThenInclude(p => p.ApplicationUser)
                // DÜZELTME 3: Sıralamayı Tarih'e göre yapıyoruz
                .OrderByDescending(pv => pv.Tarih)
                .ThenBy(pv => pv.Personel.ApplicationUser.TamAd)
                .ToList();
        }

        public List<Personel> GetAllPersoneller()
        {
            return _context.Personeller.Include(p => p.ApplicationUser).ToList();
        }
    }
}