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
    public class ProjectSingleQuery : IRequest<Project>
    {
        public int Id { get; set; }
        public class ProjectSingleQueryHandler : IRequestHandler<ProjectSingleQuery, Project>
        {
            private readonly PortfolioDbContext db;

            public ProjectSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Project> Handle(ProjectSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Projects.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
