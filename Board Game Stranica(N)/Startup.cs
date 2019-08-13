using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Board_Game_Stranica_N_.Startup))]
namespace Board_Game_Stranica_N_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
