using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Domain.Models.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public string ImagePath{ get; set; }
        public string OnlinePath{ get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime? DeletedDate { get; set; }
        public int CategoryId{ get; set; }
        public virtual Category Category{ get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
