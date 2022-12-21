using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Models.Entities.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.DataContext.Configurations.Membership
{
    public class PortfolioUserRoleEntityTypeConfiguration : IEntityTypeConfiguration<PortfolioUserRole>
    {
        public void Configure(EntityTypeBuilder<PortfolioUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
