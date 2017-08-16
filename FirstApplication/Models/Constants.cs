using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstApplication.Models
{
    public class Constants
    {
        public const string RoleNameAdmin = "Admin";
        public const string RoleNameUser = "User";
        public const string RoleNameGuest = "Guest";

        public static IdentityRole RoleAdmin = new IdentityRole { Id = "707cb935-d772-45f2-8677-f5eef0f9e94e", Name = "Admin" };
        public static IdentityRole RoleUser = new IdentityRole { Id = "5c9ae5e2-f06a-4dd1-bca5-531a961e9ddd", Name = "User" };
        public static IdentityRole RoleGuest = new IdentityRole { Id = "5578bb9e-6944-4b65-8367-eb290db8e222", Name = "Guest" };
        public static List<IdentityRole> Roles
        {
            get
            {
                List<IdentityRole> roles = new List<IdentityRole>();
                roles.Add(RoleAdmin);
                roles.Add(RoleUser);
                roles.Add(RoleGuest);

                return roles;
            }
        }
    }
}