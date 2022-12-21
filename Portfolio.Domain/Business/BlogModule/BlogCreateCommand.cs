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

namespace Portfolio.Domain.Business.BlogModule
{
    public class BlogCreateCommand : IRequest<Blog>
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public class BlogCreateCommandHandler : IRequestHandler<BlogCreateCommand, Blog>
        {
            private readonly PortfolioDbContext db;
            private readonly IHostEnvironment env;

            public BlogCreateCommandHandler(PortfolioDbContext db,IHostEnvironment env)
            {
                this.db = db;
                this.env = env;
            }
            public async Task<Blog> Handle(BlogCreateCommand request, CancellationToken cancellationToken)
            {
                var blog = new Blog
                {
                    Title = request.Title,
                    ImagePath = request.ImagePath,
                    Description = request.Description,
                    UserId = request.UserId
                };

                blog.ImagePath = request.Image.GetRandomImagePath("blog");

                await env.SaveAsync(request.Image, blog.ImagePath, cancellationToken);


                await db.Blogs.AddAsync(blog, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return blog;
            }
        }
    }
}
