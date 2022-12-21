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

namespace Portfolio.Domain.Business.SpecialtyModule
{
    public class SpecialityRemoveCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }

        public class SpecialityRemoveCommandHandler : IRequestHandler<SpecialityRemoveCommand, JsonResponse>
        {
            private readonly PortfolioDbContext db;

            public SpecialityRemoveCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<JsonResponse> Handle(SpecialityRemoveCommand request, CancellationToken cancellationToken)
            {

                var data = await db.Specialities
                    .FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedDate == null, cancellationToken);


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
