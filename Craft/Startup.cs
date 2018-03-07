using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Craft.Startup))]
namespace Craft
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
