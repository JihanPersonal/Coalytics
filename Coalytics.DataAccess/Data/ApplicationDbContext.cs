using Coalytics.Models.Auth;
using Coalytics.Models.Auth.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.EntityFrameworkCore;
namespace Coalytics.DataAccess.Data
{
    // TODO refactor out to data layer
    /// <summary>
    /// DB Context for Identity
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<CoalyticsUser>
    {
        /// <summary>
        /// Send Options to its base class.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet for CoalyticsTeam
        /// </summary>
        public DbSet<CoalyticsTeam> CoalyticsTeams { get; set; }
        /// <summary>
        /// DbSet for CoalyticsProject
        /// </summary>
        public DbSet<CoalyticsProject> CoalyticsProjects { get; set; }
        /// <summary>
        /// DbSet for FormComponentType
        /// </summary>
        public DbSet<FormComponentType> FormComponentTypes { get; set; }
        /// <summary>
        /// DbSet for FormComponent
        /// </summary>
        public DbSet<FormComponent> FormComponents { get; set; }

        public static readonly LoggerFactory CoalyticsLoggerFactory
           = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true) });

        /// <summary>
        /// Define Tables and Relationships 
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CoalyticsUser>(org =>
            {
                org.ToTable("CoalyticsUser");
            });

            builder.Entity<CoalyticsTeam>(org =>
            {
                org.HasKey(x => x.TeamId);               
            });

            builder.Entity<TeamUser>(org =>
            {
                org.HasKey(x => new { x.UserId, x.TeamId});
            });

            builder.Entity<CoalyticsProject>(org =>
            {
                org.HasKey(x => x.ProjectId);               
            });

            builder.Entity<ProjectTeam>(org =>
            {
                org.HasKey(p => new { p.ProjectId, p.TeamId});
            });

            builder.Entity<FormComponent>(org =>
            {
                org.HasKey(x => x.FormComponentId);
            });

            builder.Entity<FormComponentType>(org =>
            {
                org.HasKey(x => x.FormComponentTypeId);                
            });

            builder.Entity<ProjectFormComponent>(org =>
            {
                org.HasKey(x => new { x.ProjectId, x.FormComponentId });
            });

        }
    }
}
