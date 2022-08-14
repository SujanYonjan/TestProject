using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Core.Models;
using TestProject.Core.Models.ViewModel;

namespace TestProject.Core.Services
{
    public class SeedService
    {
        public static void SeedData(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            SeedRoles(_roleManager);
            if (_userManager.FindByNameAsync(UserRolesVM.User).Result == null)
            {
                SeedUser(_userManager, _roleManager);
            }
        }
        //Seeding the Roles
        public static void SeedRoles(RoleManager<IdentityRole> _roleManager)
        {
            if (!_roleManager.RoleExistsAsync(UserRolesVM.User).Result)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Id = "1",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "User",
                };
                IdentityResult identityResult = _roleManager.CreateAsync(identityRole).Result;
            }
        }
        //Seeding the User
        public static void SeedUser(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = "SeededUser@gmail.com",
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = "User01",
            };
            IdentityResult Userresult = _userManager.CreateAsync(user, "Password@123").Result;
            if (_roleManager.RoleExistsAsync(UserRolesVM.User).Result)
            {
                IdentityResult RoleResult = _userManager.AddToRoleAsync(user, UserRolesVM.User).Result;
            }
        }
    }
}
