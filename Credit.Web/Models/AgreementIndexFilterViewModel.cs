using System.ComponentModel.DataAnnotations;


namespace Credit.Web.Models
{   
    public class AgreementIndexFilterViewModel { 
        
        [Display(Name = "Agreement ID", Prompt = "Agreement ID")]
        public	int?	FilterAgreementId { get; set; }

        [Display(Name = "Product type", Prompt = "Product type")]
        public	string	FilterProductType { get; set; }

        [Display(Name = "Product term", Prompt = "Product term")]
        public	string	FilterProductTerm { get; set; }

        [Display(Name = "Loan amount", Prompt = "Loan amount")]
        public	int?	FilterIssueValue { get; set; }
    }
}
