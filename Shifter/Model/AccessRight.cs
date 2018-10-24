﻿using System;
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
        [Required]
        public string Access { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}