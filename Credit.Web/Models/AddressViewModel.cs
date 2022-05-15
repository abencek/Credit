using System.ComponentModel.DataAnnotations;

namespace Credit.Web.Models
{   
   public class AddressViewModel
    {
        [Display(Name="Address ID")]
        public	int	AddressId	{ get; set; }

        [Display(Name="Address type")]
        public	string AddressTypeValue	{ get; set; }

        [Display(Name="City")]
        public	string City	{ get; set; }

        [Display(Name="Street")]
        public	string Street	{ get; set; }

        [Display(Name="Zip code")]
        public	string Zip	{ get; set; }
    }
}
