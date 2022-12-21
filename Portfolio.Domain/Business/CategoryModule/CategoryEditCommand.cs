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
    public class CategoryEditCommand:IRequest<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class CategoryEditCommandHandler : IRequestHandler<CategoryEditCommand, Category>
        {
            private readonly PortfolioDbContext db;

            public CategoryEditCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Category> Handle(CategoryEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Categories.FirstOrDefaultAsync(s => s.Id == request.Id , cancellationToken);
                if (data==null)
                {
                    return null;
                }
                data.Name = request.Name;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
