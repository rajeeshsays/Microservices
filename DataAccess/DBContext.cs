using TransportService.Model;
using Microsoft.EntityFrameworkCore;


namespace TransportService.DataAccess
{
    public class TransportServiceDBContext : DbContext 
    {
        public TransportServiceDBContext(DbContextOptions<TransportServiceDBContext> options) : base(options) { }
   
        public DbSet<Driver> Driver { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }

        public DbSet<Vehicle> Vehicle{ get; set; }

        public DbSet<TransportEntry> TransportEntry { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<Party> Party { get; set; }

        public DbSet<DestinationGroup> DestinationGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransportEntry>()
       .HasOne(e => e.LocationFrom)
       .WithMany()
       .HasForeignKey(e => e.From)
       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransportEntry>()
                .HasOne(e => e.LocationTo)
                .WithMany()
                .HasForeignKey(e => e.To)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransportEntry>()
                .HasOne(e => e.ReturnDestination)
                .WithMany()
                .HasForeignKey(e => e.ReturnDestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransportEntry>()
                .HasOne(e => e.Party_Party1)
                .WithMany()
                .HasForeignKey(e => e.Party1)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<TransportEntry>()
                .HasOne(e => e.Driver)
                .WithMany()
                .HasForeignKey(e => e.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vehicle>()
            .HasOne(e => e.VehicleType)
            .WithMany()
            .HasForeignKey(e => e.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

        }

    }

}
