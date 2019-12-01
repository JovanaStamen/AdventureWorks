namespace AdventureWorks.DAL.GenericRepository
{
    public class DbFactory : IDbFactory
    {
        AdventureWorks2017Entities dbContext;

        public AdventureWorks2017Entities Init()
        {
            return dbContext ?? (dbContext = new AdventureWorks2017Entities());
        }
    }
}
