﻿using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCCMS.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly CmsUserStore _store;
		private readonly CmsUserManager _manager;

		public UserRepository()
		{
			_store = new CmsUserStore();
			_manager = new CmsUserManager();
		}

		public CmsUser GetUserByName(string username)
		{
			return _store.FindByNameAsync(username).Result;
		}

		public IEnumerable<CmsUser> GetAllUsers()
		{
			return _store.Users.ToArray();
		}

		public async Task CreateAsync(CmsUser user, string password)
		{
			await _manager.CreateAsync(user, password);
		}

		public void Delete(CmsUser user)
		{
			var result = _manager.DeleteAsync(user).Result;
		}

		public void Update(CmsUser user)
		{
			var result = _manager.UpdateAsync(user).Result;
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