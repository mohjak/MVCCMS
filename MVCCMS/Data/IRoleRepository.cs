﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCMS.Data
{
	public interface IRoleRepository : IDisposable
	{
		Task<IdentityRole> GetRoleByNameAsync(string name);
		Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
		Task CreateAsync(IdentityRole role);
	}
}