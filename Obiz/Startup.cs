using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Obiz.Startup))]
namespace Obiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
