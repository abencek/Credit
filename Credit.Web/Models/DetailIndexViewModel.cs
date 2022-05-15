using System.Collections.Generic;


namespace Credit.Web.Models
{   
    public class DetailIndexViewModel
    {  
        public CustomerViewModel CustomerInfo;

        public AddressViewModel Address1;

        public AddressViewModel Address2;

        public string ActiveTab;

        public IEnumerable<AgreementViewModel> TabAgreementsData;

        public IEnumerable<CommentViewModel> TabCommentsData;
    }
}
