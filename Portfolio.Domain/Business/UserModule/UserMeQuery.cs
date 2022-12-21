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
    public class UserMeQuery:IRequest<User>
    {
        public int Id { get; set; } = 2;
        public class UserMeQueryHandler : IRequestHandler<UserMeQuery, User>
        {
            private readonly PortfolioDbContext db;

            public UserMeQueryHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<User> Handle(UserMeQuery request, CancellationToken cancellationToken)
            {
                var data = await db.Users.Include(u => u.Experiences).Include(u=>u.Speciality).Include(u=>u.Abilities).FirstOrDefaultAsync(u=>u.Id==request.Id,cancellationToken);
                return data;
            }
        }
    }
}
