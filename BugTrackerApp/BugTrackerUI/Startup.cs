using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTrackerUI.Startup))]
namespace BugTrackerUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
