﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCCMS.Models;
using System.Threading.Tasks;
using System.Data.Entity;

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

				db.Posts.Add(model);
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

		public async Task<IEnumerable<Post>> GetAllAsync()
		{
			using (var db = new CmsContext())
			{
				return await db.Posts.Include("Author")
					.OrderByDescending(post => post.Created).ToArrayAsync();
			}
		}

		public async Task<IEnumerable<Post>> GetPostsByAuthorAsync(string authorId)
		{
			using (var db = new CmsContext())
			{
				return await db.Posts.Include("Author")
					.Where(p=>p.AuthorId == authorId)
					.OrderByDescending(post => post.Created).ToArrayAsync();
			}
		}

		public void Delete(string id)
		{
			using (var db = new CmsContext())
			{
				var post = db.Posts.SingleOrDefault(p => p.Id == id);

				if (post == null)
				{
					throw new KeyNotFoundException("The post with the id of " + id + " does not exist.");
				}

				db.Posts.Remove(post);
				db.SaveChanges();
			}
		}
	}
}