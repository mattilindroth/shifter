﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class AccessRightsGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccessRight> AccessRights { get; set; }
    }
}
