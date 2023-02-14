using MedAdvisor.Models;
using Microsoft.EntityFrameworkCore;

namespace MedAdvisor.DataAccess.MySql.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // user -> profile 
            modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId);

            // user -> allergy
            modelBuilder.Entity<User>().HasMany(x => x.Allergies)
                .WithMany(x => x.Users).
                UsingEntity(j => j.ToTable("AllergyUser"));
                

            // user -> medicine
            modelBuilder.Entity<User>().HasMany(x => x.Medicines)
                .WithMany(x => x.Users).
                UsingEntity(j => j.ToTable("MedicineUser"));

            // user -> diagnosis
            modelBuilder.Entity<User>().HasMany(x => x.Diagnoses)
                .WithMany(x => x.Users).
                UsingEntity(j => j.ToTable("DiagnosesUser"));


            // user -> vaccine
            modelBuilder.Entity<User>().HasMany(x => x.Vaccines)
                .WithMany(x => x.Users).
                UsingEntity(j => j.ToTable("VaccineUser"));


            // user -> document 
            modelBuilder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UserId);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Diagnoses> Diagnosess { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

    }
}
