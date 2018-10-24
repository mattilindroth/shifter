using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shifter.Model
{
    public class WorkshiftPart
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("WorkshiftTemplate")]
        public int TemplateId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public int DurationMinutes {get; set;}
    }
}
