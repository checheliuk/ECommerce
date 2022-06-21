using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ECommerce.WebUI.Startup))]
namespace ECommerce.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
