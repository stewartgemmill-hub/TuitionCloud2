using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TutionCloudWeb.Startup))]
namespace TutionCloudWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
