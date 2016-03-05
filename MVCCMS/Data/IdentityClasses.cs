﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace MVCCMS.Data
{
	public class CmsUserStore : UserStore<CmsUser>
	{
		public CmsUserStore()
			: this(new CmsContext())
		{ }
		public CmsUserStore(CmsContext context)
			: base(context)
		{ }
	}

	public class CmsUserManager : UserManager<CmsUser>
	{
		public CmsUserManager()
			:this(new CmsUserStore())
		{ }
		public CmsUserManager(UserStore<CmsUser> userStore)
			: base(userStore)
		{ }

		internal Task RemoveFromRoleAsync(string id, string[] roleNames)
		{
			throw new NotImplementedException();
		}
	}
}