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
    public class ExperienceEntityTypeConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Description).IsRequired().HasMaxLength(350);
            builder.Property(e => e.CompanyName).HasMaxLength(120);
            builder.Property(e => e.CompanyLocation).HasMaxLength(100);
            builder.Property(e => e.ExperienceType).IsRequired();
            builder.Property(e => e.UserId).IsRequired();
        }
    }
}
