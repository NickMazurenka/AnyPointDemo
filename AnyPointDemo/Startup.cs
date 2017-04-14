using AnyPointDemo;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace AnyPointDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
