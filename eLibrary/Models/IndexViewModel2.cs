using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace eLibrary.Models
{
    public class IndexViewModel2
    {
       // public string Search { get; set; }
        //public IPagedList<MembershipUser> Users { get; set; }
        public List<MembershipUser> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public bool IsRolesEnabled { get; set; }
    }
}