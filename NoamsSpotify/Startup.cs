using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoamsSpotify.Startup))]
namespace NoamsSpotify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
