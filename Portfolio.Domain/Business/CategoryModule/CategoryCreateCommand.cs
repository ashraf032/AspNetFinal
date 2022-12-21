using MediatR;
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
    public class CategoryCreateCommand : IRequest<Category>
    {
        public string Name { get; set; }

        public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Category>
        {
            private readonly PortfolioDbContext db;

            public CategoryCreateCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Category> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                var data = new Category
                {
                    Name = request.Name

                };

                await db.Categories.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
