using AgriManage.DataAccess.Data;

namespace AgriManage.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        // Constructor: Veritabanı bağlantısını alır
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
        }

        // Değişiklikleri veritabanına fiziksel olarak yazar
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}