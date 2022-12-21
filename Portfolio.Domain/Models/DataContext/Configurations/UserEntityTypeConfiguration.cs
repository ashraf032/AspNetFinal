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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Surname).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Age).IsRequired();
            builder.Property(u => u.Location).IsRequired().HasMaxLength(150);
            builder.Property(u => u.Longitude).IsRequired().HasPrecision(7,5);
            builder.Property(u => u.Latitude).IsRequired().HasPrecision(7,5);
            builder.Property(u => u.Phone).IsRequired().HasMaxLength(10);
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.ImagePath).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Bio).IsRequired().HasMaxLength(450);
            builder.Property(u => u.SpecialityId).IsRequired();
        }
    }
}
