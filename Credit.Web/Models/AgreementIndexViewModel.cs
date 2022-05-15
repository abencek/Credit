using System.Collections.Generic;


namespace Credit.Web.Models
{   
    public class AgreementIndexViewModel
    {  
        public IEnumerable<AgreementViewModel> Agreements;

        public AgreementIndexFilterViewModel FilterItems;

        public PagingInfoViewModel PagingInfo;
    }

}
