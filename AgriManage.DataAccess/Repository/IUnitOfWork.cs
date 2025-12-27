namespace AgriManage.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        // Veritabanına yapılan tüm değişiklikleri tek seferde kaydetmek için
        void Save();
    }
}