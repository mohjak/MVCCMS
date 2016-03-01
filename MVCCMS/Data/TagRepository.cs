using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCMS.Data
{
	public class TagRepository : ITagRepository
	{
		public void Delete(string tag)
		{
			using (var db = new CmsContext())
			{
				var posts = db.Posts.Where(p => p.CombinedTags.Contains(tag)).ToList();

				posts = posts.Where(post =>
				post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
				.ToList();

				if (!posts.Any())
				{
					throw new KeyNotFoundException("The tag " + tag + " does not exists.");
				}

				foreach (var post in posts)
				{
					post.Tags.Remove(tag);
				}

				db.SaveChanges();
			}
		}

		public void Edit(string existingTag, string newTag)
		{

			using (var db = new CmsContext())
			{
				var posts = db.Posts.Where(p => p.CombinedTags.Contains(existingTag)).ToList();

				posts = posts.Where(post => 
				post.Tags.Contains(existingTag, StringComparer.CurrentCultureIgnoreCase))
				.ToList();

				if (!posts.Any())
				{
					throw new KeyNotFoundException("The tag " + existingTag + " does not exists.");
				}

				foreach (var post in posts)
				{
					post.Tags.Remove(existingTag);
					post.Tags.Add(newTag);
				}

				db.SaveChanges();
			}
		}

		public string Get(string tag)
		{
			using (var db = new CmsContext())
			{
				var posts = db.Posts.Where(p => p.CombinedTags.Contains(tag)).ToList();

				posts = posts.Where(post => 
					post.Tags.Contains(tag, StringComparer.CurrentCultureIgnoreCase))
					.ToList();

				if (!posts.Any())
				{
					throw new KeyNotFoundException("The tag " + tag + " does not exists.");
				}

				return tag.ToLower();
			}
		}

		public IEnumerable<string> GetAll()
		{
			using (var db = new CmsContext())
			{
				var tagsCollection = db.Posts.Select(p => p.CombinedTags).ToList();
				return string.Join(",", tagsCollection).Split(',').Distinct();

				// return db.Posts.ToList().SelectMany(post => post.Tags).Distinct();
			}
		}
	}
}