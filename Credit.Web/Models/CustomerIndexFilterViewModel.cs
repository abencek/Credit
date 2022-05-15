using System.ComponentModel.DataAnnotations;


namespace Credit.Web.Models
{   
    public class CustomerIndexFilterViewModel { 
        
        [Display(Name = "Customer ID", Prompt = "Customer ID")]
        public	int?	FilterCustomerId { get; set; }

        [Display(Name = "First name", Prompt = "First name")]
        public	string	FilterFirstName { get; set; }

        [Display(Name = "Last name", Prompt = "Last name")]
        public	string	FilterLastName { get; set; }

        [Display(Name = "Personal number", Prompt = "Personal number")]
        public	string	FilterPersonalId { get; set; }
    }
}
