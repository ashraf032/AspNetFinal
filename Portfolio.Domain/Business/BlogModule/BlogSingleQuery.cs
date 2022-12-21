using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.BlogModule
{
    public class BlogSingleQuery : IRequest<Blog>
    {
        public int Id { get; set; }
        public class BlogSingleQueryHandler : IRequestHandler<BlogSingleQuery, Blog>
        {
            private readonly PortfolioDbContext db;

            public BlogSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Blog> Handle(BlogSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Blogs.Include(b=>b.User).FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedDate==null, cancellationToken);
                return data;
            }
        }
    }
}
