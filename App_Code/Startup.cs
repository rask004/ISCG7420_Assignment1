using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASP_Alt.Startup))]
namespace ASP_Alt
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
