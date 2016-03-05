using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCMS.Data
{
	public interface IUserRepository : IDisposable
	{
		Task<CmsUser> GetUserByNameAsync(string username);
		Task<IEnumerable<CmsUser>> GetAllUsersAsync();
		Task CreateAsync(CmsUser user, string password);
		Task DeleteAsync(CmsUser user);
		Task UpdateAsync(CmsUser user);
		bool VerifyUserPassword(string hashedPassword, string providedPassword);
		string HashPassword(string password);
		Task<IEnumerable<string>> GetRolesForUserAsync(CmsUser user);
		Task AddUserToRoleAsync(CmsUser newUser, string selectedRole);
		Task RemoveUserFromRoleAsync(CmsUser user, params string[] roleNames);
	}
}
