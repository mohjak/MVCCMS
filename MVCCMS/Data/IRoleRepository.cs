﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCMS.Data
{
	public interface IRoleRepository : IDisposable
	{
		IdentityRole GetRoleByName(string name);
		IEnumerable<IdentityRole> GetAllUsers();
		void Create(IdentityRole role);
	}
}