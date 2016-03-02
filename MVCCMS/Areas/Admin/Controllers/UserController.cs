using Microsoft.AspNet.Identity;
using MVCCMS.Areas.Admin.Services;
using MVCCMS.Areas.Admin.ViewModels;
using MVCCMS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCCMS.Areas.Admin.Controllers
{
	[RouteArea("admin")]
	[RoutePrefix("user")]
    public class UserController : Controller
    {
		private readonly IUserRepository _userRepository;
		private readonly IRoleRepository _roleRepository;
		private readonly UserService _users;

		public UserController()
		{
			_userRepository = new UserRepository();
			_roleRepository = new RoleRepository();
			_users = new UserService(ModelState, _userRepository, _roleRepository);
		}
        // GET: Admin/User
		[Route("")]
        public ActionResult Index()
        {
			using (var manager = new CmsUserManager())
			{
				var users = manager.Users.ToList();
				return View(users);
			}
        }

		[Route("create")]
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[Route("create")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(UserViewModel model)
		{
			if (await _users.CreateAsync(model))
			{
				return RedirectToAction("index");
			}
			else
			{
				return View();
			}
		}

		[Route("edit/{username}")]
		[HttpGet]
		public ActionResult Edit(string username)
		{
			using (var userSotre = new CmsUserStore())
			using (var userManager = new CmsUserManager(userSotre))
			{
				var user = userSotre.FindByNameAsync(username).Result;
				if (user == null)
				{
					return HttpNotFound();
				}

				var viewModel = new UserViewModel
				{
					UserName = user.UserName,
					Email = user.Email
				};
				return View(viewModel);
			}
		}

		[Route("edit/{username}")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserViewModel model)
		{
			using (var userSotre = new CmsUserStore())
			using (var userManager = new CmsUserManager(userSotre))
			{
				var user = userSotre.FindByNameAsync(model.UserName).Result;
				if (user == null)
				{
					return HttpNotFound();
				}

				if (!ModelState.IsValid)
				{
					return View(model);
				}

				if (!string.IsNullOrWhiteSpace(model.NewPassword))
				{
					if (string.IsNullOrWhiteSpace(model.CurrentPassword))
					{
						ModelState.AddModelError(string.Empty, "The current password must be supplied.");
						return View(model);
					}

					var passwordVerified = userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.CurrentPassword);
					if (passwordVerified != PasswordVerificationResult.Success)
					{
						ModelState.AddModelError(string.Empty, "The current password does not match our records.");
						return View(model);
					}

					var newHashPassword = userManager.PasswordHasher.HashPassword(model.NewPassword);
					user.PasswordHash = newHashPassword;
				}

				user.Email = model.Email;
				user.DisplayName = model.DisplayName;

				var updatedResult = userManager.UpdateAsync(user).Result;

				if (updatedResult.Succeeded)
				{
					return RedirectToAction("index");
				}

				else
				{
					ModelState.AddModelError(string.Empty, "An error occured. Please try again.");
					return View(model);
				}

			}
		}

		[Route("delete/{username}")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(string username)
		{
			using (var userSotre = new CmsUserStore())
			using (var userManager = new CmsUserManager(userSotre))
			{
				var user = userSotre.FindByNameAsync(username).Result;
				if (user == null)
				{
					return HttpNotFound();
				}

				var deletedResult = userManager.DeleteAsync(user).Result;
				return RedirectToAction("index");
			}
		}

		private bool _isDisposed;
		protected override void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				_userRepository.Dispose();
				_roleRepository.Dispose();
			}
			_isDisposed = true;

			base.Dispose(disposing);
		}
	}
}