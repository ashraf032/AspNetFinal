using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
    public class ExperienceEditCommand:IRequest<Experience>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyImagePath { get; set; }//view'a gonderende imagePath lazimdir
        public IFormFile Image { get; set; }
        public string CompanyLocation { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public int UserId { get; set; }
        public class ExperienceEditCommandHandler : IRequestHandler<ExperienceEditCommand, Experience>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public ExperienceEditCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Experience> Handle(ExperienceEditCommand request, CancellationToken cancellationToken)
            {
                var experience = await db.Experiences.FirstOrDefaultAsync(e => e.Id == request.Id  && e.EndDate==null, cancellationToken);
                if (experience == null)
                {
                    return null;
                }
                experience.Name = request.Name;
                experience.CompanyImagePath = request.CompanyImagePath;
                experience.Description = request.Description;
                experience.CompanyName = request.CompanyName;
                experience.CompanyLocation = request.CompanyLocation;
                experience.UserId = request.UserId;
                experience.ExperienceType = request.ExperienceType;

                if (request.Image == null)
                {
                    goto save;
                }
                string newImageName = request.Image.GetRandomImagePath("experience");

                await env.SaveAsync(request.Image, newImageName, cancellationToken);

                env.ArchiveImage(experience.CompanyImagePath);
                experience.CompanyImagePath = newImageName;

            save:
                await db.SaveChangesAsync(cancellationToken);
                return experience;
            }
        }
    }
}
