﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCMS.Models
{
	public class CmsUser : IdentityUser
	{
		public string DisplayName { get; set; }
	}
}