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

namespace Portfolio.Domain.Business.SkillModule
{
    public class SkillEditCommand:IRequest<Skill>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public string Description { get; set; }
        public SkillType SkillType { get; set; }
        public int UserId { get; set; }
        public class SkillEditCommandHandler : IRequestHandler<SkillEditCommand, Skill>
        {
            private readonly PortfolioDbContext db;

            public SkillEditCommandHandler(PortfolioDbContext db)
            {
                this.db = db;
            }
            public async Task<Skill> Handle(SkillEditCommand request, CancellationToken cancellationToken)
            {
                var data = await db.Skills.FirstOrDefaultAsync(s => s.Id == request.Id , cancellationToken);
                if (data==null)
                {
                    return null;
                }
                data.Name = request.Name;
                data.Percent = request.Percent;
                data.Description = request.Description;
                data.SkillType = request.SkillType;
                data.UserId = request.UserId;

                await db.SaveChangesAsync(cancellationToken);
                return data;
            }
        }
    }
}
