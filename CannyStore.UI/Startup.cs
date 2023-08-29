using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CannyStore.UI.Startup))]
namespace CannyStore.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
