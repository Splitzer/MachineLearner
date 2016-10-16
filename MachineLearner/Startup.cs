using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MachineLearner.Startup))]
namespace MachineLearner
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
