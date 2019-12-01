using AdventureWorks.DAL.GenericRepository;
using System.Data.Entity;

namespace AdventureWorks.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private AdventureWorks2017Entities dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public AdventureWorks2017Entities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public void Dispose() // IDisposable implementation
        {
            dbContext.Dispose();
        }
    }
}
