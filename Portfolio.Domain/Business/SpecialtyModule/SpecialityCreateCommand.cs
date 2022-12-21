using MediatR;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.SpecialtyModule
{
    public class SpecialityCreateCommand : IRequest<Speciality>
    {
        public string Name { get; set; }
        public string Bio { get; set; }

        public class SpecialityCreateCommandHandler : IRequestHandler<SpecialityCreateCommand, Speciality>
        {
            private readonly PortfolioDbContext db;

            public SpecialityCreateCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Speciality> Handle(SpecialityCreateCommand request, CancellationToken cancellationToken)
            {
                var data = new Speciality
                {
                    Name = request.Name,
                    Bio = request.Bio
                };

                await db.Specialities.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
