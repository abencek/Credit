using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Credit.Data.App.Models;

namespace Credit.Data.App
{
    /// <summary>
    /// Repository for additional data about customers
    /// </summary>
    
    public class EFAppCommentRepository:IAppCommentRepository
    {
        private readonly AppDbContext context;

        public EFAppCommentRepository(AppDbContext ctx)
        {
            context=ctx;
        }

        public IEnumerable<Comment> Comments => context.Comments.Include(x=>x.Customer).Include(x=>x.CommentType);
       
    }
}
