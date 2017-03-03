namespace Visions.Data.Contracts
{
    public interface IVisionsDbContext
    {
        void InitializeDb();

        void InitializeIdentity();

        void SaveChanges();
    }
}
