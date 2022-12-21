using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Percent { get; set; }
        public string Description { get; set; }
        public SkillType SkillType { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
