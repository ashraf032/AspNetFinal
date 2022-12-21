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

namespace Portfolio.Domain.Business.ServiceModule
{
    public class ServiceSingleQuery : IRequest<Service>
    {
        public int Id { get; set; }
        public class ServiceSingleQueryHandler : IRequestHandler<ServiceSingleQuery, Service>
        {
            private readonly PortfolioDbContext db;

            public ServiceSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServiceSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Services.FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
