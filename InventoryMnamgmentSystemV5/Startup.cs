using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventoryMnamgmentSystemV5.Startup))]
namespace InventoryMnamgmentSystemV5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
