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
    public class AbilitySingleQuery : IRequest<Ability>
    {
        public int Id { get; set; }
        public class AbilitySingleQueryHandler : IRequestHandler<AbilitySingleQuery, Ability>
        {
            private readonly PortfolioDbContext db;

            public AbilitySingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Ability> Handle(AbilitySingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Abilities.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
