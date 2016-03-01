using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MVCCMS.Models
{
	public static class StringExtentions
	{
		public static string MakeUrlFriendly(this string value)
		{
			value.ToLowerInvariant().Replace(" ", "-");
			value = Regex.Replace(value, @"[^0-9a-z-]", string.Empty);

			return value;
		}
	}
}