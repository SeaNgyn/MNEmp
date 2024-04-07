using Microsoft.EntityFrameworkCore;
using WebFormL1.Models;
using WebFormL1.SeedData;

namespace WebFormL1.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Ethnic> Ethnices { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<Ethnic>().HasData(
                new SeedEthnic().EthnicGroups
            );
            modelBuilder.Entity<Job>().HasData(
                new SeedJob().Jobs
           );
            modelBuilder.Entity<Employee>().HasData(
                new SeedEmployee().Employees
           );
            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasData(new SeedAdministrativeCountrySubdivision().Provinces);
                entity.ToTable("Province");
                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasData(new SeedAdministrativeCountrySubdivision().Districts);
                entity.ToTable("District");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceId");
                entity.HasOne(e => e.Province)
                        .WithMany(x => x.Districts)
                        .HasForeignKey(e => e.ProvinceId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_province_id");

            });

            modelBuilder.Entity<Commune>(entity =>
            {
                entity.HasData(new SeedAdministrativeCountrySubdivision().Communes);
                entity.ToTable("Commune");
                entity.HasOne(e => e.District)
                        .WithMany(x => x.Communes)
                        .HasForeignKey(d => d.DistrictId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("fk_district_id");
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                 .HasColumnName("iD");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.Name)
                    .HasColumnName("Name");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.DateOfBirth)
                    .HasColumnName("DateOfBirth");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.EthnicId)
                    .HasColumnName("EthnicId");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.JobId)
                    .HasColumnName("JobId");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.IdentityCardNumber)
                    .HasColumnName("IdentityCardNumber");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.PhoneNumber)
                    .HasColumnName("PhoneNumber");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.ProvinceId)
                    .HasColumnName("ProvinceId");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.DistrictId)
                    .HasColumnName("DistrictId");

                modelBuilder.Entity<Employee>()
                    .Property(e => e.CommuneId)
                    .HasColumnName("CommuneId");

                modelBuilder.Entity<Employee>()
                    .HasOne(x => x.Commune)
                    .WithMany()
                    .HasForeignKey(x => x.CommuneId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Employee>()
                    .HasOne(x => x.District)
                    .WithMany()
                    .HasForeignKey(x => x.DistrictId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Employee>()
                    .HasOne(x => x.Province)
                    .WithMany()
                    .HasForeignKey(x => x.ProvinceId)
                    .OnDelete(DeleteBehavior.NoAction);

                modelBuilder.Entity<Employee>()
                    .HasOne(x => x.Job)
                    .WithMany()
                    .HasForeignKey(x => x.JobId);

                modelBuilder.Entity<Employee>()
                    .HasOne(x => x.Ethnic)
                    .WithMany()
                    .HasForeignKey(x => x.EthnicId);
            });
            modelBuilder.Entity<Degree>(entity =>
            {
                entity.HasOne(x => x.Employee)
                    .WithMany(e => e.Degrees) // Assuming you have a property named Degrees in the Employee class
                    .HasForeignKey(x => x.EmployeeId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(x => x.Province)
                    .WithMany()
                    .HasForeignKey(x => x.ProvinceId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<Department>()
            .HasKey(d => d.DepartmentId);
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasMany(e => e.Employees).
                WithOne(e => e.Department).HasForeignKey(e=>e.DepartmentId);
            });
            modelBuilder.Entity<Attendance>()
                .HasKey(d => d.AttendanceId);
            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasOne(e => e.Employee).WithMany().
                HasForeignKey(e => e.EmployeeId);
            });
        }
    }
}
