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

namespace Portfolio.Domain.Business.AbilityModule
{
    public class AbilityAllQuery:IRequest<List<Ability>>
    {
        public class AbilityAllQueryHandler : IRequestHandler<AbilityAllQuery, List<Ability>>
        {
            private readonly PortfolioDbContext db;

            public AbilityAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Ability>> Handle(AbilityAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Abilities
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
