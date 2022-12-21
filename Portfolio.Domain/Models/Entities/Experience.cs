using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities
{
    public class Experience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
        public string CompanyImagePath { get; set; }
        public string CompanyLocation { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? EndDate { get; set; }
        public ExperienceType ExperienceType { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
