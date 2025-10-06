using Microsoft.EntityFrameworkCore;
using Tibaks_Backend.Auth;
using Tibaks_Backend.Models;

namespace Tibaks_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<HealthcareWorker> HealthcareWorkers {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Patient>(entity => {
                entity.ToTable("Patients");
                entity.HasKey(t => t.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired();
                    
                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CivilStatus_Mother)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });
            // Add any additional model configurations here
        }
        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
    }
}
