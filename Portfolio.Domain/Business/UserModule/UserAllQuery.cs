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

namespace Portfolio.Domain.Business.UserModule
{
    public class UserAllQuery:IRequest<List<User>>
    {
        public class UserAllQueryHandler : IRequestHandler<UserAllQuery, List<User>>
        {
            private readonly PortfolioDbContext db;

            public UserAllQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<List<User>> Handle(UserAllQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Users.ToListAsync(cancellationToken);
                return data;
            }
        }
    }
}
