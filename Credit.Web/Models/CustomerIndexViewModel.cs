using System.Collections.Generic;


namespace Credit.Web.Models
{   
    public class CustomerIndexViewModel
    {  
        public IEnumerable<CustomerViewModel> Customers;

        public CustomerIndexFilterViewModel FilterItems;

        public PagingInfoViewModel PagingInfo;
    }

}
