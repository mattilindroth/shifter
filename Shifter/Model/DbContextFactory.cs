using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Shifter.Model
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer(Startup.ConnectionString,
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DataContext).GetTypeInfo().Assembly.GetName().Name));

            return new DataContext(builder.Options);
        }
    }
}
