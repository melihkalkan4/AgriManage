using AgriManage.BusinessLogic.Dtos;
using System.Collections.Generic;

namespace AgriManage.BusinessLogic.Services
{
    public interface IAnalizService
    {
        // Eski metodlar (Eğer başka yerde kullanıyorsan kalsın, yoksa silebilirsin)
        int GetToplamPersonel();
        int GetAktifGorevSayisi();
        double GetToplamArazi();
        int GetBugunkuVardiyaSayisi();
        Dictionary<string, double> GetTarlaDagilimi();
        Dictionary<string, decimal> GetAylikGiderler();

        // 🔥 YENİ METODUMUZ 🔥
        AnalizDto GetDetayliAnaliz();
    }
}