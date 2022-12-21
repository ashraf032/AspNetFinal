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

namespace Portfolio.Domain.Business.ExperienceModule
{
    public class ExperienceAllQuery:IRequest<List<Experience>>
    {
        public class ExperienceAllQueryHandler : IRequestHandler<ExperienceAllQuery, List<Experience>>
        {
            private readonly PortfolioDbContext db;

            public ExperienceAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Experience>> Handle(ExperienceAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Experiences.OrderByDescending(e=>e.StartDate)
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
