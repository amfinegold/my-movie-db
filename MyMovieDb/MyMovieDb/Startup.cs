using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyMovieDb.Startup))]
namespace MyMovieDb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
