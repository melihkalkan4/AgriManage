using AgriManage.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Services
{
    public interface ITarlaService
    {
        List<Tarla> GetAllTarlalar(string userId);
        Tarla GetTarlaById(int id);
        void CreateTarla(Tarla tarla);
        void UpdateTarla(Tarla tarla);
        void DeleteTarla(int id);

        List<Lokasyon> GetAllLokasyonlar();
        List<Urun> GetAllUrunler();
        List<Sezon> GetAktifSezonlar();

        void CreateEkimPlani(EkimPlani ekimPlani);
        List<EkimPlani> GetAktifEkimler(string userId);
        EkimPlani GetSonEkimByTarlaId(int tarlaId);

        // YENİ EKLENEN METOT İMZASI
        List<EkimPlani> GetGecmisEkimler(string userId);

        void HasatYap(int ekimPlaniId, decimal gerceklesenVerim, DateTime hasatTarihi);
    }
}