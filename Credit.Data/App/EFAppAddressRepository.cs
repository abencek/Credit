using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Credit.Data.App.Models;


namespace Credit.Data.App
{
    /// <summary>
    /// Repository for customers contact data
    /// </summary>
    
    public class EFAppAddressRepository:IAppAddressRepository
    {
        private readonly AppDbContext context;

        public EFAppAddressRepository(AppDbContext ctx)
        {
            context=ctx;
        }

        public IEnumerable<Address> Addresses => context.Addresses.Include(x=>x.Customer).Include(x=>x.AddressType);
       
    }
}
