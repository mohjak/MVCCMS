using MVCCMS.Data;
using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCMS.Areas.Admin.Controllers
{
	[RouteArea("admin")]
	[RoutePrefix("post")]
	public class PostController : Controller
	{
		private readonly IPostRepository _repository;

		public PostController()
			: this(new PostRepository())
		{ }

		public PostController(IPostRepository repository)
		{
			_repository = repository;
		}

		// GET: Admin/Post
		[Route("")]
		public ActionResult Index()
		{
			var posts = _repository.GetAll();
			return View(posts);
		}

		// /admin/post/create
		[HttpGet]
		[Route("create")]
		public ActionResult Create()
		{
			return View(new Post());
		}

		// /admin/post/create
		[HttpPost]
		[Route("create")]
		public ActionResult Create(Post model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (string.IsNullOrWhiteSpace(model.Id))
			{
				model.Id = model.Title;
			}

			model.Id = model.Id.MakeUrlFriendly();
			model.Tags = model.Tags.Select(tag => tag.MakeUrlFriendly()).ToList();
			model.Created = DateTime.Now;
			model.AuthorId = "12c035a5-412e-433e-ab18-516420075265";
			try
			{
				_repository.Create(model);

				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				ModelState.AddModelError("key", e);
				return View(model);
			}

		}

		// /admin/post/edit/post-to-edit
		[HttpGet]
		[Route("edit/{postId}")]
		public ActionResult Edit(string postId)
		{
			var post = _repository.Get(postId);

			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}

		// /admin/post/edit/post-to-edit
		[HttpPost]
		[Route("edit/{postId}")]
		public ActionResult Edit(string postId, Post model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			if (string.IsNullOrWhiteSpace(model.Id))
			{
				model.Id = model.Title;
			}

			model.Id = model.Id.MakeUrlFriendly();
			model.Tags = model.Tags.Select(tag => tag.MakeUrlFriendly()).ToList();

			try
			{
				_repository.Edit(postId, model);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException e)
			{
				return HttpNotFound();
			}
			catch (Exception e)
			{
				ModelState.AddModelError(string.Empty, e.Message);
				return View(model);
			}
		}

		// /admin/post/delete/post-to-edit
		[HttpGet]
		[Route("delete/{postId}")]
		public ActionResult Delete(string postId)
		{
			var post = _repository.Get(postId);

			if (post == null)
			{
				return HttpNotFound();
			}
			return View(post);
		}

		// /admin/post/delete/post-to-edit
		[HttpPost]
		[Route("delete/{postId}")]
		public ActionResult Delete(string postId, string foo)
		{
			try
			{
				_repository.Delete(postId);

				return RedirectToAction("Index");
			}
			catch (KeyNotFoundException e)
			{
				return HttpNotFound();
			}
		}

	}
}