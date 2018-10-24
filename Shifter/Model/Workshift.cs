using System;
using System.Collections.Generic;
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


    }
}
