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

namespace Portfolio.Domain.Business.SpecialtyModule
{
    public class SpecialityAllQuery:IRequest<List<Speciality>>
    {
        public class SpecialityAllQueryHandler : IRequestHandler<SpecialityAllQuery, List<Speciality>>
        {
            private readonly PortfolioDbContext db;

            public SpecialityAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<Speciality>> Handle(SpecialityAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Specialities.Where(s=>s.DeletedDate==null)
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
