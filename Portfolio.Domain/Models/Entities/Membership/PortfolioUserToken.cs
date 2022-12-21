using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities.Membership
{
    public class PortfolioUserToken:IdentityUserToken<int>
    {
    }
}
