using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCCMS.Data
{
	public interface IPostRepository
	{
		Post Get(string id);
		void Edit(string id, Post udatedItem);
		void Create(Post model);
		void Delete(string id);
		IEnumerable<Post> GetAll();
	}
}
