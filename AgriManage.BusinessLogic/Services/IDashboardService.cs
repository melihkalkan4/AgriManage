using AgriManage.DataAccess.Models;
using AgriManage.Models;

namespace AgriManage.BusinessLogic.Services
{
    public interface IDashboardService
    {
        // Admin veya Personel durumuna göre Dashboard verisini hazırlar
        Task<DashboardViewModel> GetDashboardDataAsync(string userId, bool isAdmin);
    }
}