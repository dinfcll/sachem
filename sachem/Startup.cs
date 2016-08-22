using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sachem.Startup))]
namespace sachem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
