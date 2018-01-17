using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC框架理解.Startup))]
namespace MVC框架理解
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
