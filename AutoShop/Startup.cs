using System;
using Microsoft.Owin;
using Owin;
[assembly: OwinStartup(typeof(AutoShop.Startup))]
namespace AutoShop
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}