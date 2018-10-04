using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(GlobalGeobits.ChatApp.web.Startup))]
namespace GlobalGeobits.ChatApp.web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}