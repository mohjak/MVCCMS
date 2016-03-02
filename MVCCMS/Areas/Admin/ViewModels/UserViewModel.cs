using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCMS.Areas.Admin.ViewModels
{
	public class UserViewModel
	{
		[Required]
		public string UserName { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string DisplayName { get; set; }
		public string CurrentPassword { get; set; }
		[Compare("ConfirmPassword", ErrorMessage ="The new password and the confirmation password don not match.")]
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }

	}
}