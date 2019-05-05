using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Auth_Demo.Startup))]
namespace MVC_Auth_Demo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
