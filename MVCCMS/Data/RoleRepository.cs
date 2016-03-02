using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCMS.Data
{
	public class RoleRepository : IRoleRepository
	{
		private readonly RoleStore<IdentityRole> _store;
		private readonly RoleManager<IdentityRole> _manager;

		public RoleRepository()
		{
			_store = new RoleStore<IdentityRole>();
			_manager = new RoleManager<IdentityRole>(_store);
		}

		public IdentityRole GetRoleByName(string name)
		{
			return _store.FindByNameAsync(name).Result;
		}

		public IEnumerable<IdentityRole> GetAllUsers()
		{
			return _store.Roles.ToArray();
		}

		public void Create(IdentityRole role)
		{
			var result = _manager.Create(role);
		}

		private bool _disposed = false;
		public void Dispose()
		{
			if (!_disposed)
			{
				_store.Dispose();
				_manager.Dispose();
			}

			_disposed = true;
		}
	}
}