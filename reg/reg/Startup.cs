using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(reg.Startup))]
namespace reg
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
