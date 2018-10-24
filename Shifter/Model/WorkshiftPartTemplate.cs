using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class WorkshiftPartTemplate
    {
        [Key]
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [DefaultValue("true")]
        public bool IsWork { get; set; }
        [DefaultValue("0")]
        public int DefaultDurationMinutes { get; set; }
        [Required]
        [DefaultValue("CURRENT_TIMESTAMP")]
        public DateTime DefaultStartTime { get; set; }
    }
}
