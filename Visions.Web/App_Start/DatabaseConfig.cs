using System.Data.Entity;
using Visions.Data;

namespace Visions.Web.App_Start
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfDbContext, Visions.Data.Migrations.Configuration>());
            EfDbContext.Create();
        }
    }
}