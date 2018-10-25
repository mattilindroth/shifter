using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class AccessRight
    {
        [Key]
        public int Id { get; set; }
        public AccessRightsGroup AccessRightGroup { get; set; }
        public AccessRightTemplate AccessRightTemplate {get; set;}
    }
}
