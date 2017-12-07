using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FIAPWSDoBemRCService.Startup))]

namespace FIAPWSDoBemRCService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}