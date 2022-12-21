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

namespace Portfolio.Domain.Business.SkillModule
{
    public class SkillSingleQuery : IRequest<Skill>
    {
        public int Id { get; set; }
        public class SkillSingleQueryHandler : IRequestHandler<SkillSingleQuery, Skill>
        {
            private readonly PortfolioDbContext db;

            public SkillSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Skill> Handle(SkillSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Skills.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
