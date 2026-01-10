using AgriManage.DataAccess.Models;
using System; // DateTime kullanabilmek için bunu eklemeyi unutma
using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Services
{
    public interface IVardiyaService
    {
        List<Vardiya> GetVardiyaTanimlari();
        void CreateVardiyaTanim(Vardiya vardiya);

        // 👇 DÜZELTİLMESİ GEREKEN SATIR BURASI 👇
        // Eski Hali: void VardiyaAta(int personelId, int vardiyaId, DayOfWeek gun);
        // Yeni Hali:
        void VardiyaAta(int personelId, int vardiyaId, DateTime tarih);

        List<PersonelVardiya> TumProgramiGetir();
        List<Personel> GetAllPersoneller();
    }
}