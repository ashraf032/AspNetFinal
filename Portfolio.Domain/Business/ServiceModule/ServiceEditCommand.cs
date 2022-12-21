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
    public class ServiceEditCommand:IRequest<Service>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconPath { get; set; }
        public int SpecialityId { get; set; }
        public class ServiceEditCommandHandler : IRequestHandler<ServiceEditCommand, Service>
        {
            private readonly PortfolioDbContext db;

            public ServiceEditCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Service> Handle(ServiceEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Services.FirstOrDefaultAsync(s => s.Id == request.Id , cancellationToken);
                if (data==null)
                {
                    return null;
                }
                data.Name = request.Name;
                data.Description = request.Description;
                data.IconPath = request.IconPath;
                data.SpecialityId = request.SpecialityId;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
