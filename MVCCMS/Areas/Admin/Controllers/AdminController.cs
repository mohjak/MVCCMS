using Microsoft.Owin.Security;
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
	[Authorize]
	public class AdminController : Controller
	{
		private readonly IUserRepository _users;

		public AdminController() : this(new UserRepository()) { }

		public AdminController(IUserRepository userRepository)
		{
			_users = userRepository;
		}
		// GET: Admin/Admin
		[Route("")]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("login")]
		[AllowAnonymous]
		public async Task<ActionResult> login()
		{
			return View();
		}

		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
		public async Task<ActionResult> login(LoginViewModel model)
		{
			var user = await _users.GetLoginUserAsync(model.UserName, model.Password);

			if (user == null)
			{
				ModelState.AddModelError(string.Empty, "The user with the supplied credentials is already exists.");
			}

			var authManager = HttpContext.GetOwinContext().Authentication;
			var userIdentity = await _users.CreateIdentityAsync(user);

			authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, userIdentity);
			return RedirectToAction("index");
		}

		[Route("logout")]
		public async Task<ActionResult> logout()
		{
			var authManager = HttpContext.GetOwinContext().Authentication;
			authManager.SignOut();
			return RedirectToAction("index", "home");
		}
	}
}