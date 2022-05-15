using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Credit.Web.Models;


namespace Credit.Web.Controllers
{
    /// <summary>
    /// Application Login endpoints
    /// Access is enabled only for signed users
    /// </summary>
    
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController (UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr){

            userManager=userMgr;
            signInManager=signInMgr;

        }
        

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login (string returnUrl) {

            return View(new AccountLoginViewModel(){
                ReturnUrl = returnUrl
            });

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login ([FromForm] AccountLoginViewModel loginModel) {
            
           //Check for Login model data
           if (ModelState.IsValid)
           {
                //User sign in process
               IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
               if (user!=null)
               {
                   await signInManager.SignOutAsync();
                   if ((await signInManager.PasswordSignInAsync(user,loginModel.Password,loginModel.RememberMe,false)).Succeeded){

                        return Redirect(loginModel?.ReturnUrl ?? Url.Action(nameof(Login)));

                    }
               }
           }

           ModelState.AddModelError(String.Empty,"Incorrect user name or password!");

           return View(loginModel);

        }


        [HttpGet]
        public async Task<RedirectResult> Logout (string returnUrl="/"){

            await signInManager.SignOutAsync();

            return Redirect(returnUrl);

        }
    }
}
