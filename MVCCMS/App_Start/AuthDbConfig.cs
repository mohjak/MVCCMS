using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCCMS.Data;
using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCMS.App_Start
{
	public class AuthDbConfig
	{
		public async static Task RegisterAdmin()
		{
			using(var users = new UserRepository())
			{
				var user = users.GetUserByName("admin");

				if (user == null)
				{
					var adminUser = new CmsUser
					{
						UserName = "admin",
						Email = "admin@cms.com",
						DisplayName = "Administrator"
					};

					await users.CreateAsync(adminUser, "Passw0rd1234");
				}
			}

			using (var roles = new RoleRepository())
			{
				if (roles.GetRoleByName("admin") == null)
				{
					roles.Create(new IdentityRole("admin"));
				}
				if (roles.GetRoleByName("editor") == null)
				{
					roles.Create(new IdentityRole("editor"));
				}
				if (roles.GetRoleByName("author") == null)
				{
					roles.Create(new IdentityRole("author"));
				}
			}
		}
	}
}