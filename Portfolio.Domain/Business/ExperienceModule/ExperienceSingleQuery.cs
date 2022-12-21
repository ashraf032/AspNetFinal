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
    public class ExperienceSingleQuery : IRequest<Experience>
    {
        public int Id { get; set; }
        public class ExperienceSingleQueryHandler : IRequestHandler<ExperienceSingleQuery, Experience>
        {
            private readonly PortfolioDbContext db;

            public ExperienceSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Experience> Handle(ExperienceSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Experiences.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
