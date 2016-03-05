﻿using MVCCMS.Areas.Admin.ViewModels;
using MVCCMS.Data;
using MVCCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCCMS.Areas.Admin.Services
{
	public class UserService
	{
		private readonly IUserRepository _users;
		private readonly IRoleRepository _roles;
		private readonly ModelStateDictionary _modelState;

		public UserService(ModelStateDictionary modelState, 
			IUserRepository userRepository, IRoleRepository roleRepository)
		{
			_modelState = modelState;
			_users = userRepository;
			_roles = roleRepository;
		}
		
		public async Task<UserViewModel> GetUserByNameAsync(string name)
		{
			var user = await _users.GetUserByNameAsync(name);

			if (user == null)
			{
				return null;
			}

			var viewModel = new UserViewModel
			{
				UserName = user.UserName,
				Email = user.Email,
				DisplayName = user.DisplayName
			};

			return viewModel;
		}

		public async Task<bool> CreateAsync(UserViewModel model)
		{
			if (!_modelState.IsValid)
			{
				return false;
			}

			var existingUser = await _users.GetUserByNameAsync(model.UserName);

			if (existingUser != null)
			{
				_modelState.AddModelError(string.Empty, "The user already exits!");
				return false;
			}
			
			if (string.IsNullOrWhiteSpace(model.NewPassword))
			{
				_modelState.AddModelError(string.Empty, "You must type a password.");
				return false;
			}

			var newUser = new CmsUser
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				UserName = model.UserName
			};

			await _users.CreateAsync(newUser, model.NewPassword);
			return true;
		}

		public async Task<bool> UpdateUser(UserViewModel model)
		{
			var user = await _users.GetUserByNameAsync(model.UserName);

			if (user == null)
			{
				_modelState.AddModelError(string.Empty, "The specified user does not exist.");
				return false;
			}
			if (!_modelState.IsValid)
			{
				return false;
			}

			if (!string.IsNullOrWhiteSpace(model.NewPassword))
			{
				if (!string.IsNullOrWhiteSpace(model.CurrentPassword))
				{
					_modelState.AddModelError(string.Empty, "The current password must be supplied.");
					return false;
				}

				var passwordVerified = _users.VerifyUserPassword(user.PasswordHash, model.CurrentPassword);

				if (!passwordVerified)
				{
					_modelState.AddModelError(string.Empty, "The current password does not match our records.");
					return false;
				}

				var newHashPassword = _users.HashPassword(model.NewPassword);

				user.PasswordHash = newHashPassword;
			}

			user.Email = model.Email;
			user.DisplayName = model.DisplayName;

			await _users.UpdateAsync(user);

			return true;
			
		}

		public async Task DeleteAsync(string username)
		{
			var user = await _users.GetUserByNameAsync(username);

			if (user == null)
			{
				return;
			}

			await _users.DeleteAsync(user);
		}
	}
}