using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Portfolio.Domain.AppCode.Extensions;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.ExperienceModule
{
    public class ExperienceCreateCommand : IRequest<Experience>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyImagePath { get; set; }
        public IFormFile Image { get; set; }
        public string CompanyLocation { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public int UserId { get; set; }

        public class ExperienceCreateCommandHandler : IRequestHandler<ExperienceCreateCommand, Experience>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public ExperienceCreateCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Experience> Handle(ExperienceCreateCommand request, CancellationToken cancellationToken)
            {
                var experience = new Experience
                {
                    Name = request.Name,
                    CompanyImagePath = request.CompanyImagePath,
                    Description = request.Description,
                    CompanyName=request.CompanyName,
                    CompanyLocation=request.CompanyLocation,
                    UserId =request.UserId,
                    ExperienceType=request.ExperienceType
                };
                    experience.CompanyImagePath = request.Image.GetRandomImagePath("experience");
                    await env.SaveAsync(request.Image, experience.CompanyImagePath, cancellationToken);
                


                await db.Experiences.AddAsync(experience, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return experience;
            }
        }
    }
}
