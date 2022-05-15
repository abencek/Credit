using Microsoft.EntityFrameworkCore;
using Credit.Data.App.Models;

namespace Credit.Data.App
{
    /// <summary>
    /// Context for application database
    /// </summary> 
    
    public class AppDbContext:DbContext
    {
         public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options){}
            
            public DbSet<Agreement> Agreements {get;set;}
            public DbSet<Customer> Customers {get;set;}
            public DbSet<CustomerStatus> CustomerStatuses {get;set;}
            public DbSet<Address> Addresses {get;set;}
            public DbSet<AddressType> AddressTypes {get;set;}
            public DbSet<Comment> Comments {get;set;}
            public DbSet<CommentType> CommentTypes {get;set;}
    }
}
