using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Coalytics.DataAccess.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContextFactory()
        {
        }


        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // TODO - make configurable
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(
                "Server=(localdb)\\ProjectsV13;Database=Coalytics;Trusted_Connection=True;MultipleActiveResultSets=true; ");

            return new ApplicationDbContext(builder.Options);
        }
    }
}
