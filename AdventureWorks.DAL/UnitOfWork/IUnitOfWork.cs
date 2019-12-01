using AdventureWorks.DAL.GenericRepository;

namespace AdventureWorks.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}