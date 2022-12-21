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

namespace Portfolio.Domain.Business.BlogModule
{
    public class BlogEditCommand:IRequest<Blog>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }//view'a gonderende imagePath lazimdir
        public string Description { get; set; }
        public int UserId { get; set; }
        public class BlogEditCommandHandler : IRequestHandler<BlogEditCommand, Blog>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public BlogEditCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Blog> Handle(BlogEditCommand request, CancellationToken cancellationToken)
            {
                var blog = await db.Blogs.FirstOrDefaultAsync(b => b.Id == request.Id  && b.DeletedDate==null, cancellationToken);
                if (blog == null)
                {
                    return null;
                }
                blog.Title = request.Title;
                blog.Description = request.Description;
                blog.UserId = request.UserId;

                if (request.Image == null)
                {
                    goto save;
                }
                string newImageName = request.Image.GetRandomImagePath("blog");

                await env.SaveAsync(request.Image, newImageName, cancellationToken);

                env.ArchiveImage(blog.ImagePath);
                blog.ImagePath = newImageName;

            save:
                await db.SaveChangesAsync(cancellationToken);
                return blog;
            }
        }
    }
}
