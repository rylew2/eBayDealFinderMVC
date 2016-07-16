using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eBayDealFinder.Startup))]
namespace eBayDealFinder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
