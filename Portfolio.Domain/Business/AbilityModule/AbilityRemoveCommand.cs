using MediatR;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.AppCode.Infrastructure;
using Portfolio.Domain.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.AbilityModule
{
    public class AbilityRemoveCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }

        public class AbilityRemoveCommandHandler : IRequestHandler<AbilityRemoveCommand, JsonResponse>
        {
            private readonly PortfolioDbContext db;

            public AbilityRemoveCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(AbilityRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.Abilities
                    .FirstOrDefaultAsync(a => a.Id == request.Id,cancellationToken);

                if (data == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                db.Abilities.Remove(data);
                await db.SaveChangesAsync(cancellationToken);

                return new JsonResponse
                {
                    Error = false,
                    Message = "Ugurludur"
                };
            }
        }
    }
}
