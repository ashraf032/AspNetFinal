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

namespace Portfolio.Domain.Business.SkillModule
{
    public class SkillRemoveCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }

        public class SkillRemoveCommandHandler : IRequestHandler<SkillRemoveCommand, JsonResponse>
        {
            private readonly PortfolioDbContext db;

            public SkillRemoveCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(SkillRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.Skills
                    .FirstOrDefaultAsync(s => s.Id == request.Id,cancellationToken);

                if (data == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                db.Skills.Remove(data);
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
