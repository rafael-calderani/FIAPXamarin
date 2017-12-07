using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(API.WorkshopDoBem.Startup))]

namespace API.WorkshopDoBem {
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureMobileApp(app);
        }
    }
}