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
    public class UserSingleQuery:IRequest<User>
    {
        public int Id { get; set; }
        public class UserSingleQueryHandler : IRequestHandler<UserSingleQuery, User>
        {
            private readonly PortfolioDbContext db;

            public UserSingleQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<User> Handle(UserSingleQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                return data;
            }
        }
    }
}
