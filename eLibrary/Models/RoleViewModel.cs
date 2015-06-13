using System.Collections.Generic;
using System.Web.Security;

namespace JobHustler.Models
{
	public class RoleViewModel
	{
		public string Role { get; set; }
		public IDictionary<string, MembershipUser> Users { get; set; }
	}
}