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

namespace Portfolio.Domain.Business.ProjectModule
{
    public class ProjectRemoveCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }

        public class ProjectRemoveCommandHandler : IRequestHandler<ProjectRemoveCommand, JsonResponse>
        {
            private readonly PortfolioDbContext db;

            public ProjectRemoveCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(ProjectRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.Projects
                    .FirstOrDefaultAsync(p => p.Id == request.Id && p.DeletedDate==null,cancellationToken);

                if (data == null)
                {
                    return new JsonResponse
                    {
                        Error = true,
                        Message = "Qeyd tapilmadi"
                    };
                }

                data.DeletedDate = DateTime.UtcNow.AddHours(4);
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
