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

namespace Portfolio.Domain.Business.CategoryModule
{
    public class CategoryAllQuery:IRequest<List<Category>>
    {
        public class CategoryAllQueryHandler : IRequestHandler<CategoryAllQuery, List<Category>>
        {
            private readonly PortfolioDbContext db;

            public CategoryAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Category>> Handle(CategoryAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Categories
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
