using System.Collections.Generic;
using Credit.Data.App.Models;

namespace Credit.Data.App
{
    public interface IAppCustomerRepository
    {
        IEnumerable <Customer> Customers {get;}
        IEnumerable<CustomerStatus> CustomerStatuses {get;}
        bool SaveCustomer(Customer customer);
    }
}
