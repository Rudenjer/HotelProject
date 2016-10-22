using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelProject.Startup))]
namespace HotelProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
