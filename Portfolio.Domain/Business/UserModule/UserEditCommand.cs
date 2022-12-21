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
    public class UserEditCommand:IRequest<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Location { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int? Experience { get; set; }
        public string Degree { get; set; }
        public string CareerLevel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Bio { get; set; }
        public class UserEditCommandHandler : IRequestHandler<UserEditCommand, User>
        {
            private readonly PortfolioDbContext db;

            public UserEditCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<User> Handle(UserEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
                if (data==null)
                {
                    return null;
                }
                data.Name = request.Name;
                data.Surname = request.Surname;
                data.Location = request.Location;
                data.Longitude = request.Longitude;
                data.Latitude = request.Latitude;
                data.Experience = request.Experience;
                data.Degree = request.Degree;
                data.CareerLevel = request.CareerLevel;
                data.Phone = request.Phone;
                data.Email = request.Email;
                data.Bio = request.Bio;


                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
