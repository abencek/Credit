

namespace Credit.Web.Models
{
    public class IdentityUserViewModel
    { 
        public string UserName {get; set;}

        public string Email {get; set;}

        public string PhoneNumber {get; set;}

        public bool LockoutEnabled {get; set;}

        public string RoleNames {get; set;}
    }
}
