using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities
{
    public enum SkillType
    {
        [Display(Name ="Hard Skill")]
        Hard=0,
        [Display(Name = "Soft Skill")]
        Soft,
        [Display(Name = "Speciality Skill")]
        Speciality
    }
}
