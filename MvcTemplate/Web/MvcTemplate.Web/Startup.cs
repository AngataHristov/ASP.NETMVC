﻿using Microsoft.Owin;

using Owin;

[assembly: OwinStartup(typeof(MvcTemplate.Web.Startup))]

namespace MvcTemplate.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
