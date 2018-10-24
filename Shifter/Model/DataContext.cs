using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Options = options;
        }

        //public DbContextOptions<DataContext> Options { get; }

        public DbSet<User> Users { get; set; }
        public DbSet<WorkshiftPartTemplate> WorkshiftPartTemplate { get; set; }
        public DbSet<WorkshiftPart> WorkshiftPart { get; set; }
        public DbSet<WorkshiftTemplate> WorkshiftTemplate { get; set; }
        public DbSet<Workshift> Workshift { get; set; }

        public DbSet<AccessRight> AccessRights { get; set; }
        public DbSet<AccessRightsGroup> AccessRightsGroup { get; set; }

    }
}
