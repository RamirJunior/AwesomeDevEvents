using AwesomeDevEvents.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Persistence {
    public class DevEventsDbContext : DbContext {

        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options): base(options) {

        }

        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {

            builder.Entity<DevEvent>(e => {
                e.HasKey(dev => dev.Id);
                e.Property(dev => dev.Title).IsRequired(false);
                
                e.Property(dev => dev.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");

                e.Property(dev => dev.StartDate).HasColumnName("start_date");
                e.Property(dev => dev.EndDate).HasColumnName("end_date");

                e.HasMany(dev => dev.Speakers)
                    .WithOne()
                    .HasForeignKey(dev => dev.Id);
                    
            });

            builder.Entity<DevEventSpeaker>(e => {
                e.HasKey(devs => devs.Id);
            });
        }
    }
}
