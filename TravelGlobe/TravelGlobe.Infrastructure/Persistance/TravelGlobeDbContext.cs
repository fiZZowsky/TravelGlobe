using Microsoft.EntityFrameworkCore;
using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Infrastructure.Persistance
{
    public class TravelGlobeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Trip> Trips { get; set; }

        public TravelGlobeDbContext(DbContextOptions<TravelGlobeDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("main");

            // User
            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.DisplayName).IsRequired();
            });

            // Country
            modelBuilder.Entity<Country>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).IsRequired();
            });

            // Airport
            modelBuilder.Entity<Airport>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(a => a.Name).IsRequired();
                b.Property(a => a.City);
                b.Property(a => a.CountryCode).IsRequired();
                b.OwnsOne(a => a.Code, cfg =>
                {
                    cfg.Property(v => v.Iata).HasColumnName("Iata").HasMaxLength(3).IsRequired();
                });
                b.OwnsOne(a => a.Location, cfg =>
                {
                    cfg.Property(v => v.Latitude).HasColumnName("Latitude").IsRequired();
                    cfg.Property(v => v.Longitude).HasColumnName("Longitude").IsRequired();
                });
            });

            // Trip
            modelBuilder.Entity<Trip>(b =>
            {
                b.HasKey(t => t.Id);

                b.Property(t => t.CountryCode).IsRequired();
                b.Property(t => t.DistanceKm).IsRequired();

                // Klucze obce
                b.HasOne<User>()
                    .WithMany(u => u.Trips)
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne<Country>()
                    .WithMany(c => c.Trips)
                    .HasForeignKey(t => t.CountryCode)
                    .OnDelete(DeleteBehavior.Restrict);

                // Lotniska
                b.HasOne<Airport>()
                    .WithMany()
                    .HasForeignKey(t => t.DepartureAirportId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<Airport>()
                    .WithMany()
                    .HasForeignKey(t => t.ArrivalAirportId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<Airport>()
                    .WithMany()
                    .HasForeignKey(t => t.ReturnDepartureAirportId)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne<Airport>()
                    .WithMany()
                    .HasForeignKey(t => t.ReturnArrivalAirportId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
