using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCCMS.Models;

namespace MVCCMS.Data
{
	public class PostRepository : IPostRepository
	{
		public void Create(Post model)
		{
			using (var db = new CmsContext())
			{
				var post = db.Posts.SingleOrDefault(p => p.Id == model.Id);

				if (post != null)
				{
					throw new ArgumentException("A post with id of " + model.Id + " already exists.");
				}

				db.Posts.Attach(model);
				db.SaveChanges();
			}
		}

		public void Edit(string id, Post udatedItem)
		{
			using (var db = new CmsContext())
			{
				var post = db.Posts.SingleOrDefault(p => p.Id == id);

				if (post == null)
				{
					throw new KeyNotFoundException("A post with id of "
						+ id + " does not exist in the data source.");
				}

				post.Id = udatedItem.Id;
				post.Title = udatedItem.Title;
				post.Content = udatedItem.Content;
				post.Published = udatedItem.Published;
				post.Tags = udatedItem.Tags;
				db.SaveChanges();
			}
		}

		public Post Get(string id)
		{
			using (var db = new CmsContext())
			{
				return db.Posts.Include("Author").SingleOrDefault(post => post.Id == id);
			}
		}

		public IEnumerable<Post> GetAll()
		{
			using (var db = new CmsContext())
			{
				return db.Posts.Include("Author")
					.OrderByDescending(post => post.Created).ToArray();
			}
		}
	}
}