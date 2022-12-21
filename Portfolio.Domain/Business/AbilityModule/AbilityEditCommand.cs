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
    public class AbilityEditCommand:IRequest<Ability>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public class AbilityEditCommandHandler : IRequestHandler<AbilityEditCommand, Ability>
        {
            private readonly PortfolioDbContext db;

            public AbilityEditCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Ability> Handle(AbilityEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Abilities.FirstOrDefaultAsync(a => a.Id == request.Id , cancellationToken);
                if (data==null)
                {
                    return null;
                }
                data.Name = request.Name;
                data.UserId = request.UserId;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
