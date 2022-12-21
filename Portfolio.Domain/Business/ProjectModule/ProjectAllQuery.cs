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

namespace Portfolio.Domain.Business.ProjectModule
{
    public class ProjectAllQuery:IRequest<List<Project>>
    {
        public class ProjectAllQueryHandler : IRequestHandler<ProjectAllQuery, List<Project>>
        {
            private readonly PortfolioDbContext db;

            public ProjectAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Project>> Handle(ProjectAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Projects.Where(b=>b.DeletedDate==null).Include(p=>p.Category)
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
