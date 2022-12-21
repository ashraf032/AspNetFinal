using MediatR;
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
    public class ServiceCreateCommand : IRequest<Service>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int SpecialityId { get; set; }

        public class ServiceCreateCommandHandler : IRequestHandler<ServiceCreateCommand, Service>
        {
            private readonly PortfolioDbContext db;

            public ServiceCreateCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServiceCreateCommand request, CancellationToken cancellationToken)
            {
                var data = new Service
                {
                    Name = request.Name,
                    Description = request.Description,
                    SpecialityId = request.SpecialityId,
                    IconPath = request.IconPath

                };

                await db.Services.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
