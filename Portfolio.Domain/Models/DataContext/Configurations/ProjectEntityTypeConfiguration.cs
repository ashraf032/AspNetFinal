using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.DataContext.Configurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p=>p.Name).IsRequired().HasMaxLength(150);
            builder.Property(p=>p.ImagePath).IsRequired().HasMaxLength(150);
            builder.Property(p=>p.OnlinePath).IsRequired().HasMaxLength(250);
            builder.Property(p=>p.CategoryId).IsRequired();
        }
    }
}
