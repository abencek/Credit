using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Newtonsoft.Json;

using Credit.Web.Models;
using Credit.Data.Identity;



namespace Credit.Web.Controllers
{
    /// <summary>
    /// Admin tools for managing users accounts
    /// </summary>

    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        private readonly IdentityDbContext context;

        public AdminController(UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr, IdentityDbContext ctx)
        {
            userManager = userMgr;
            roleManager = roleMgr;
            context = ctx;
        }

        public IActionResult Index()
        {

            ViewBag.SideBarItemSelected = "Accounts administration";

            IEnumerable<IdentityUserViewModel> users =
                    from u in context.Users
                    let query = (from ur in context.UserRoles
                                 where ur.UserId.Equals(u.Id)
                                 join r in context.Roles on ur.RoleId equals r.Id
                                 select r.Name).ToList()
                    select new IdentityUserViewModel
                    {
                        UserName = u.UserName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        LockoutEnabled = u.LockoutEnabled,
                        RoleNames = query.FirstOrDefault(),
                    };


            IEnumerable<IdentityRoleViewModel> roles = 
                roleManager.Roles.Select(x =>
                    new IdentityRoleViewModel{
                        Id = x.Id,
                        Name = x.Name,
                        NormalizedName = x.NormalizedName
                    }
            );

            //Populate data for dropdowns
            var dropdowns = new
            {
                LockoutEnabled = new string[] { "True", "False" },
                RoleNames = roleManager.Roles.Select(x => x.Name).ToArray().Prepend("")
            };
            var stringWriter = new System.IO.StringWriter();
            using (var writer = new JsonTextWriter(stringWriter))
            {
                var serializer = new JsonSerializer();
                writer.QuoteName = false;
                serializer.Serialize(writer, dropdowns);
            }
            var dropdownsJson = stringWriter.ToString();
            
            //Create Admin model object
            AdminIndexViewModel model = new()
            {
                Users = users.OrderBy(x => x.UserName).ToList(),
                Roles = roles.OrderBy(x => x.Name).ToList(),
                DropdownsData = dropdownsJson
            };

            return View(model);
        }
    }
}
