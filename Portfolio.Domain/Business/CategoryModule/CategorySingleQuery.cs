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
    public class CategorySingleQuery : IRequest<Category>
    {
        public int Id { get; set; }
        public class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
        {
            private readonly PortfolioDbContext db;

            public CategorySingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Categories.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
