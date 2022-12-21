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
    public class BlogAllQuery:IRequest<List<Blog>>
    {
        public class BlogAllQueryHandler : IRequestHandler<BlogAllQuery, List<Blog>>
        {
            private readonly PortfolioDbContext db;

            public BlogAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Blog>> Handle(BlogAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Blogs.Include(b=>b.User)
                    .ToListAsync(cancellationToken);
                if (data==null)
                {
                    return null;
                }
                return data;
            }
        }
    }
}
