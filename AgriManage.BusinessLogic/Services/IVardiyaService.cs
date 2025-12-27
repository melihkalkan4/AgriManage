using AgriManage.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Services
{
    public interface IVardiyaService
    {
        List<Vardiya> GetVardiyaTanimlari();
        void CreateVardiyaTanim(Vardiya vardiya);
        void VardiyaAta(int personelId, int vardiyaId, DayOfWeek gun);
        List<PersonelVardiya> TumProgramiGetir();
        List<Personel> GetAllPersoneller();
    }
}