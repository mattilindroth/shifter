using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class Workshift
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("WorkshiftTemplate")]
        public int TemplateId { get; set; }
        public Organization Organization { get; set; }
        [Required]
        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}
