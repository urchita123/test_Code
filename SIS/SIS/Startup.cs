using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SIS.Startup))]
namespace SIS
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
