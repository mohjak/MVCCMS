﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(MVCCMS.Startup))]

namespace MVCCMS
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/admin/login")
			});
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
		}
	}
}
