using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Credit.Web.Models;


namespace Credit.Web.Controllers
{
    /// <summary>
    /// User account settings management
    /// Endpoints for viewing profile data and password change
    /// </summary>
    
    [Authorize]
    public class AccountSettingsController : Controller
    {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountSettingsController (UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr){

            userManager=userMgr;
            signInManager=signInMgr;
            
        }
        

        [HttpGet]
        public async Task<ActionResult> Index () {

            ViewBag.SideBarItemSelected="User profile";

            IdentityUser user = await userManager.GetUserAsync(User);
            AccountSettingsIndexViewModel model = new()
            {
                UserName=user.UserName,
                Email=user.Email,
                PhoneNumber=user.PhoneNumber
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword (string userName, string oldPassword, string newPassword1, string newPassword2){

                if(String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(oldPassword) || 
                        String.IsNullOrEmpty(newPassword1) || String.IsNullOrEmpty(newPassword2)){  

                    ModelState.AddModelError("", "You must fill in all fields!");
                }

                if(ModelState.IsValid && newPassword1==newPassword2){
                    IdentityUser user=await userManager.FindByNameAsync(userName);
                    if (user!=null){
                        IdentityResult result = await userManager.ChangePasswordAsync(user,oldPassword,newPassword1);
                           if (result.Succeeded)
                            {
                                return Ok();
                            }
                    }
                }

                return BadRequest();
        }
  
    }
}
