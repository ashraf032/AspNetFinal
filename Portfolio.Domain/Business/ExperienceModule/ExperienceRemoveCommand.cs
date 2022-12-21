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

namespace Portfolio.Domain.Business.ExperienceModule
{
    public class ExperienceRemoveCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }

        public class ExperienceRemoveCommandHandler : IRequestHandler<ExperienceRemoveCommand, JsonResponse>
        {
            private readonly PortfolioDbContext db;

            public ExperienceRemoveCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ExperienceRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.Experiences
                    .FirstOrDefaultAsync(p => p.Id == request.Id && p.EndDate==null,cancellationToken);

                if (data == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                data.EndDate = DateTime.UtcNow.AddHours(4);
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
