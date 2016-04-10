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
			using (var roles = new RoleRepository())
			{
				if (await roles.GetRoleByNameAsync("admin") == null)
				{
					await roles.CreateAsync(new IdentityRole("admin"));
				}
				if (await roles.GetRoleByNameAsync("editor") == null)
				{
					await roles.CreateAsync(new IdentityRole("editor"));
				}
				if (await roles.GetRoleByNameAsync("author") == null)
				{
					await roles.CreateAsync(new IdentityRole("author"));
				}
			}

			using (var users = new UserRepository())
			{
				var user1 = await users.GetUserByNameAsync("admin");

				if (user1 == null)
				{
					var adminUser = new CmsUser
					{
						UserName = "admin",
						Email = "admin@cms.com",
						DisplayName = "Administrator"
					};

					await users.CreateAsync(adminUser, "Passw0rd1234");
					await users.AddUserToRoleAsync(adminUser, "admin");
				}

				var user2 = await users.GetUserByNameAsync("author");
				if (user2 == null)
				{
					var authorUser = new CmsUser()
					{
						UserName = "author",
						Email = "author@cms.com",
						DisplayName = "Author"
					};

					await users.CreateAsync(authorUser, "P@ssw0rd1234");
					await users.AddUserToRoleAsync(authorUser, "author");
				}

				var user3 = await users.GetUserByNameAsync("editor");
				if (user3 == null)
				{
					var editorUser = new CmsUser()
					{
						UserName = "editor",
						Email = "editor@cms.com",
						DisplayName = "Editor"
					};

					await users.CreateAsync(editorUser, "P@ssw0rd1234");
					await users.AddUserToRoleAsync(editorUser, "author");
				}
			}		
		}
	}
}