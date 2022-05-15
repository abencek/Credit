using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Credit.Api.Dtos;
using Credit.Api.Mapping;

namespace Credit.Web.Controllers
{
    /// <summary>
    /// Admin API for managing user accounts with create, delete end edit functionality
    /// </summary>
    
    [ApiController]
    [Authorize]
    [Route("adminapi/[action]")]
    public class AdminController : ControllerBase
    {
         private readonly UserManager<IdentityUser> userManager;
         private readonly RoleManager<IdentityRole> roleManager;


        public AdminController (UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr){
            userManager=userMgr;
            roleManager=roleMgr;
        }
    

        [HttpPost]
        public async Task<ActionResult> SaveUser ([FromForm] IdentityUserDto userDto){

            IdentityResult result;
            
            if(ModelState.IsValid){

                IdentityUser user= await userManager.FindByNameAsync(userDto.UserName);

                if(user==null){
                    //Create new user
                    user = Mapper.Instance.Map<IdentityUser>(userDto);

                    result = await userManager.CreateAsync(user,userDto.UserName);

                    if (result.Succeeded && userDto.RoleNames!=null)
                    {
                        //Assign roles to the user
                        result = await userManager.AddToRoleAsync(user,userDto.RoleNames);
                    }
                }
                else{
                    //Update existing user
                    user.Email=userDto.Email;
                    user.PhoneNumber=userDto.PhoneNumber;
                    user.LockoutEnabled=userDto.LockoutEnabled;

                    result = await userManager.UpdateAsync(user); 

                    if (result.Succeeded)
                    {
                        //Assign roles to the user
                        IList<string> roles = await userManager.GetRolesAsync(user); 
                        if(result.Succeeded && roles.Contains(userDto.RoleNames)==false){
                            result = await userManager.RemoveFromRolesAsync(user,roles);
                            if (result.Succeeded && userDto.RoleNames!=null && 
                                    await roleManager.RoleExistsAsync(userDto.RoleNames)){
                                result = await userManager.AddToRoleAsync(user, userDto.RoleNames);
                            }
                        }
                    } 
                }

                if (result.Succeeded) {
                    return Ok();
                }
            }
           
            return BadRequest();

        }


        [HttpDelete]
        public async Task<ActionResult> DeleteUser ([FromForm] string name){

            if(name=="Admin") {
                return BadRequest();
            }

            IdentityResult result;

            if(ModelState.IsValid){

                IdentityUser user= await userManager.FindByNameAsync(name);

                if(user!=null){
                    result = await userManager.DeleteAsync(user);
                    if (result.Succeeded) {
                        return NoContent();
                    }
                }
            }

            return BadRequest();

        }


        [HttpPost]
        public async Task<ActionResult> SaveRole ([FromForm] IdentityRoleDto roleDto){

            IdentityResult result;
            
            if(ModelState.IsValid){

                IdentityRole role = await roleManager.FindByNameAsync(roleDto.Name);

                if(role == null){
                    //Create new role
                    role = Mapper.Instance.Map<IdentityRole>(roleDto);
                    result = await roleManager.CreateAsync(role);
                }
                else{
                    //Update existing role
                    role.Name = roleDto.Name;
                    result = await roleManager.UpdateAsync(role);
                }

                if (result.Succeeded) {
                    return Ok();
                }
            }
           
            return BadRequest();
            
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteRole ([FromForm] string name){

            if(name=="Admins") {
                return BadRequest();
            }

            IdentityResult result;

            if(ModelState.IsValid){

                IdentityRole role = await roleManager.FindByNameAsync(name);

                if(role != null){
                    result = await roleManager.DeleteAsync(role);
                    if (result.Succeeded) {
                        return NoContent();
                    }
                }

            }

            return BadRequest();

        }

    }
}
