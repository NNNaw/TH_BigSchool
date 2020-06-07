using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TH_BigSchool.Startup))]
namespace TH_BigSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
