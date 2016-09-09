using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Prototype_Web_App.Startup))]
namespace Prototype_Web_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
