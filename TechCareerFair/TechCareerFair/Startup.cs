using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TechCareerFair.Startup))]
namespace TechCareerFair
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
