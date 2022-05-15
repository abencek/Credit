using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Credit.Data.App.Models;


namespace Credit.Data.App
{
    /// <summary>
    /// Repository for customers personal data
    /// </summary>
    
    public class EFAppCustomerRepository:IAppCustomerRepository
    {
        private readonly AppDbContext context;

        public EFAppCustomerRepository(AppDbContext ctx)
        {
            context=ctx;
        }

        public IEnumerable<CustomerStatus> CustomerStatuses => context.CustomerStatuses;

        public IEnumerable<Customer> Customers => context.Customers.Include(x=>x.CustomerStatus);

        public bool SaveCustomer (Customer customer) {  
            
            context.Update(customer);

            try
            {
                context.SaveChanges();
                return true;
            }
            catch  
            {   
                return false;
            }              
        }   
    }
}
