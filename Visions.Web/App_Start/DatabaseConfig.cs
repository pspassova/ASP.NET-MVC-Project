using System.Data.Entity;
using Visions.Data;

namespace Visions.Web.App_Start
{
    public class DatabaseConfig
    {
        public static void InitializeDatabase()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<VisionsDbContext, Visions.Data.Migrations.Configuration>());
            VisionsDbContext.Create().InitializeDb();
        }
    }
}