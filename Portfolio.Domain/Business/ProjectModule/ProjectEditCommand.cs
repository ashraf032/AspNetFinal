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

namespace Portfolio.Domain.Business.ProjectModule
{
    public class ProjectEditCommand:IRequest<Project>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }//view'a gonderende imagePath lazimdir
        public string OnlinePath { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public class ProjectEditCommandHandler : IRequestHandler<ProjectEditCommand, Project>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public ProjectEditCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Project> Handle(ProjectEditCommand request, CancellationToken cancellationToken)
            {
                var project = await db.Projects.FirstOrDefaultAsync(p => p.Id == request.Id  && p.DeletedDate==null, cancellationToken);
                if (project==null)
                {
                    return null;
                }
                project.Name = request.Name;
                project.OnlinePath = request.OnlinePath;
                project.UserId = request.UserId;
                project.CategoryId = request.CategoryId;

                if (request.Image == null)
                {
                    goto save;
                }
                string newImageName = request.Image.GetRandomImagePath("project");

                await env.SaveAsync(request.Image, newImageName, cancellationToken);

                env.ArchiveImage(project.ImagePath);
                project.ImagePath = newImageName;

            save:
                await db.SaveChangesAsync(cancellationToken);
                return project;
            }
        }
    }
}
