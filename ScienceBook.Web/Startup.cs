using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScienceBook.Web.Startup))]
namespace ScienceBook.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
