using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities
{
    public enum ExperienceType
    {
        [Display(Name ="Professional Experience")]
        Professional=0,
        [Display(Name ="Academic Background")]
        Academic
    }
}
