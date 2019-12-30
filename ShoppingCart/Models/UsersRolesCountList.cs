using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart.Models
{
    public class UsersRolesCountList
    {
        public IdentityRole Role { get; set; }
        public int NoOfUsers { get; set; } 
    }
}