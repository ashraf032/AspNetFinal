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

namespace Portfolio.Domain.Business.ProjectModule
{
    public class ProjectCreateCommand : IRequest<Project>
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public string OnlinePath { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, Project>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public ProjectCreateCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Project> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
            {
                var project = new Project
                {
                    Name = request.Name,
                    ImagePath = request.ImagePath,
                    OnlinePath = request.OnlinePath,
                    UserId=request.UserId,
                    CategoryId=request.CategoryId
                };
                
                project.ImagePath = request.Image.GetRandomImagePath("project");

                await env.SaveAsync(request.Image, project.ImagePath, cancellationToken);


                await db.Projects.AddAsync(project, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return project;
            }
        }
    }
}
