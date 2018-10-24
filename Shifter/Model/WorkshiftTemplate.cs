using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class WorkshiftTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<WorkshiftPart> WorkshiftParts { get; set; }
    }
}
