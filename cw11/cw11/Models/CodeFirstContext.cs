using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace cw11.Models
{
    public class CodeFirstContext : DbContext
    {
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Medicament> Medicament { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicament { get; set; }

        public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

        public CodeFirstContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>(e =>
            {
                e.HasKey(a => a.IdDoctor).HasName("Doctor_PK");

                //e.Property(a => a.IdDoctor).ValueGeneratedNever();
                e.Property(a => a.FirstName).HasMaxLength(100).IsRequired();
                e.Property(a => a.LastName).HasMaxLength(100).IsRequired();
                e.Property(a => a.Email).HasMaxLength(100).IsRequired();
            });


            modelBuilder.Entity<Medicament>(e =>
            {
                e.HasKey(a => a.IdMedicament).HasName("Medicament_PK");

                e.Property(a => a.Name).HasMaxLength(100).IsRequired();
                e.Property(a => a.Description).HasMaxLength(100).IsRequired();
                e.Property(a => a.Type).HasMaxLength(100).IsRequired();
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasKey(a => a.IdPatient).HasName("Patient_PK");

                e.Property(a => a.FirstName).HasMaxLength(100).IsRequired();
                e.Property(a => a.LastName).HasMaxLength(100).IsRequired();
                e.Property(a => a.Birthdate).IsRequired();
            });

            modelBuilder.Entity<Prescription>(e =>
            {
                e.HasKey(a => a.IdPrescription).HasName("Prescription_PK");

                e.Property(a => a.Date).IsRequired();
                e.Property(a => a.DueDate).IsRequired();

                e.HasOne(a => a.Patient)
                    .WithMany(a => a.Prescriptions)
                    .HasForeignKey(a => a.IdPatient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Prescription_Patient");

                e.HasOne(a => a.Doctor)
                     .WithMany(a => a.Prescriptions)
                     .HasForeignKey(a => a.IdDoctor)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("Prescription_Doctor");
            });

            modelBuilder.Entity<Prescription_Medicament>(e =>
            {
                //e.ToTable("Prescription_Medicament"); => wiem ze tak powinno byc ale na filmie bylo pokazne zbyt pozno to a juz zrobilem
                // baze i nie chciałem zmieniać aby czegoś nie zepsuć

                //e.HasKey(a => a.IdMedicament).HasName("PM_Med_PK");
                //e.HasKey(a => a.IdPrescription).HasName("PM_Pres_PK");
                e.HasKey(a => new { a.IdMedicament, a.IdPrescription }).HasName("PM_PK");

                e.Property(a => a.Dose);
                e.Property(a => a.Details).HasMaxLength(100).IsRequired();

                e.HasOne(a => a.Medicament)
                    .WithMany(a => a.prescription_Medicaments)
                    .HasForeignKey(a => a.IdMedicament)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PM_Med");

                e.HasOne(a => a.Prescription)
                    .WithMany(a => a.prescription_Medicaments)
                    .HasForeignKey(a => a.IdPrescription)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PM_Pres");
            });
            AddDataToTable(modelBuilder);
        }
        public static void AddDataToTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor {IdDoctor = 1, FirstName = "Jan", LastName = "Kowalski", Email = "JanKowalski@amorek.pl"},
                new Doctor {IdDoctor = 2, FirstName = "Hubert", LastName = "Nowak", Email = "HubertNowak@LuxMed.com"}
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament {IdMedicament = 1, Name = "Xanax", Description = "Na ból głowy", Type = "Słabe leki"},
                new Medicament {IdMedicament = 2, Name = "Ibupar", Description = "Na ból d", Type = "Mocne leki"}
                );

            modelBuilder.Entity<Patient>().HasData(
                new Patient {IdPatient = 1, FirstName = "Byku", LastName = "Bykowski", Birthdate = DateTime.Parse("06-06-1966")},
                new Patient {IdPatient = 2, FirstName = "Adam", LastName = "Nowak", Birthdate = DateTime.Parse("10-10-1996")}
                );

            modelBuilder.Entity<Prescription>().HasData(
                new Prescription {IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Parse("10-10-2025"), IdPatient = 1, IdDoctor = 1},
                new Prescription {IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Parse("06-06-2022"), IdPatient = 2, IdDoctor = 2}
                );

            modelBuilder.Entity<Prescription_Medicament>().HasData(
                new Prescription_Medicament { Dose = 1, Details = "Brać ile wlezie", IdMedicament = 1, IdPrescription = 1},
                new Prescription_Medicament { Dose = 10, Details = "Nie więcej niż 1 rocznie", IdMedicament = 2, IdPrescription = 2}
                );
        }
    }
}
