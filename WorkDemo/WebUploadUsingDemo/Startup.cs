using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebUploadUsingDemo.Startup))]
namespace WebUploadUsingDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
