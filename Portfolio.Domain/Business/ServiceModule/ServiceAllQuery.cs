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
    public class ServiceAllQuery:IRequest<List<Service>>
    {
        public class ServiceAllQueryHandler : IRequestHandler<ServiceAllQuery, List<Service>>
        {
            private readonly PortfolioDbContext db;

            public ServiceAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Service>> Handle(ServiceAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Services
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
