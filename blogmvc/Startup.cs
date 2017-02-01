using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(blogmvc.Startup))]
namespace blogmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
