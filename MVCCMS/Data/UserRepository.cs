using Microsoft.AspNet.Identity;
using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

		public async Task<CmsUser> GetUserByNameAsync(string username)
		{
			return await _store.FindByNameAsync(username);
		}

		public async Task<IEnumerable<CmsUser>> GetAllUsersAsync()
		{
			return await _store.Users.ToArrayAsync();
		}

		public async Task CreateAsync(CmsUser user, string password)
		{
			await _manager.CreateAsync(user, password);
		}

		public async Task DeleteAsync(CmsUser user)
		{
			await _manager.DeleteAsync(user);
		}

		public async Task UpdateAsync(CmsUser user)
		{
			await _manager.UpdateAsync(user);
		}
		
		public bool VerifyUserPassword(string hashedPassword, string providedPassword)
		{
			return _manager.PasswordHasher.VerifyHashedPassword(hashedPassword, providedPassword) == 
				PasswordVerificationResult.Success;
		}
		public string HashPassword(string password)
		{
			return _manager.PasswordHasher.HashPassword(password);
		}

		public async Task AddUserToRoleAsync(CmsUser user, string role)
		{
			await _manager.AddToRoleAsync(user.Id, role);
		}

		public async Task<IEnumerable<string>> GetRolesForUserAsync(CmsUser user)
		{
			return await _manager.GetRolesAsync(user.Id);
		}

		public async Task RemoveUserFromRoleAsync(CmsUser user, params string[] roleNames)
		{
			await _manager.RemoveFromRoleAsync(user.Id, roleNames);
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