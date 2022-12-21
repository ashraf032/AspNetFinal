using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.DataContext.Configurations.Membership
{
    public class PortfolioUserEntityTypeConfiguration : IEntityTypeConfiguration<PortfolioUser>
    {
        public void Configure(EntityTypeBuilder<PortfolioUser> builder)
        {
            builder.ToTable("Users", "Membership");
        }
    }
}
