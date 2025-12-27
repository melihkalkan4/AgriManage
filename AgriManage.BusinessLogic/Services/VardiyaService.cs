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

        public void VardiyaAta(int personelId, int vardiyaId, DayOfWeek gun)
        {
            var mevcut = _context.PersonelVardiyalari
                .FirstOrDefault(p => p.PersonelId == personelId && p.Gun == gun);

            if (mevcut != null)
            {
                mevcut.VardiyaId = vardiyaId;
                _context.PersonelVardiyalari.Update(mevcut);
            }
            else
            {
                var atama = new PersonelVardiya { PersonelId = personelId, VardiyaId = vardiyaId, Gun = gun };
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
                .OrderBy(pv => pv.Personel.ApplicationUser.TamAd)
                .ThenBy(pv => pv.Gun)
                .ToList();
        }

        public List<Personel> GetAllPersoneller()
        {
            return _context.Personeller.Include(p => p.ApplicationUser).ToList();
        }
    }
}