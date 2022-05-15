using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Credit.Data.App.Models;


namespace Credit.Data.App
{
    /// <summary>
    /// Repository for agreements data
    /// </summary>
 
    public class EFAppAgreementRepository:IAppAgreementRepository
    {
        private readonly AppDbContext context;

        public EFAppAgreementRepository(AppDbContext ctx)
        {
            context=ctx;
        }

        public IEnumerable<Agreement> Agreements => context.Agreements.Include(x=>x.Customer);

        public bool SaveAgreement(Agreement agreement){

            context.Update(agreement);

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
