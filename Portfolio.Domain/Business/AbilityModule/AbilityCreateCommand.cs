using MediatR;
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
    public class AbilityCreateCommand : IRequest<Ability>
    {
        public string Name { get; set; }
        public int UserId { get; set; }

        public class AbilityCreateCommandHandler : IRequestHandler<AbilityCreateCommand, Ability>
        {
            private readonly PortfolioDbContext db;

            public AbilityCreateCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Ability> Handle(AbilityCreateCommand request, CancellationToken cancellationToken)
            {
                var data = new Ability
                {
                    Name = request.Name,
                    UserId = request.UserId

                };

                await db.Abilities.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
