using AgriManage.DataAccess.Models;

namespace AgriManage.BusinessLogic.Services
{
    public interface IAnalizService
    {
        // Kullanıcı ID'sine göre tüm analiz verisini tek pakette (ViewModel) döner
        AnalizViewModel GetGenelAnaliz(string userId);
    }
}