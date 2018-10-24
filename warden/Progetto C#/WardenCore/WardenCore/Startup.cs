using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WardenCore.Startup))]
namespace WardenCore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
