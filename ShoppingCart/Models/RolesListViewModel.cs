using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class RolesListViewModel
    {
        public IdentityRole UserRoles { get; set; }
        public List<UsersRolesCountList> RolesList = new List<UsersRolesCountList>();
    }
}