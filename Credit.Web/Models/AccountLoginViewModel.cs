using System.ComponentModel.DataAnnotations;

namespace Credit.Web.Models
{
    public class AccountLoginViewModel
    {
        [Display(Name="Username")]
        [Required]
        public string Name { get; set; }

        [Display(Name="Password")]
        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; } = false;

        public string ReturnUrl { get; set; } = "/";
    }
}
