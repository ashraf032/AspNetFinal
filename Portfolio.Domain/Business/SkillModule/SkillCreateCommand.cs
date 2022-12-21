using MediatR;
using Portfolio.Domain.Models.DataContext;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Domain.Business.SkillModule
{
    public class SkillCreateCommand : IRequest<Skill>
    {
        public string Name { get; set; }
        public int Percent { get; set; }
        public string Description { get; set; }
        public SkillType SkillType { get; set; }
        public int UserId { get; set; }

        public class SkillCreateCommandHandler : IRequestHandler<SkillCreateCommand, Skill>
        {
            private readonly PortfolioDbContext db;

            public SkillCreateCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Skill> Handle(SkillCreateCommand request, CancellationToken cancellationToken)
            {
                var data = new Skill
                {
                    Name = request.Name,
                    Percent = request.Percent,
                    Description=request.Description,
                    SkillType=request.SkillType,
                    UserId=request.UserId

                };

                await db.Skills.AddAsync(data, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);

                return data;
            }
        }
    }
}
