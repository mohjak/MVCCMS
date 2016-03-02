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
		CmsUser GetUserByName(string username);
		IEnumerable<CmsUser> GetAllUsers();
		Task CreateAsync(CmsUser user, string password);
		void Delete(CmsUser user);
		void Update(CmsUser user);
	}
}
