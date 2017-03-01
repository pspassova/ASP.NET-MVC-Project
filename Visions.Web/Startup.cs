using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Visions.Web.Startup))]
namespace Visions.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
