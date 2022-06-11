using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efDataBase.Models
{
    public class s21962Context : DbContext
    {

        protected s21962Context()
        {

        }
            

        public s21962Context(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> prescription_Medicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(p =>
            {
                p.HasKey(e => e.IdPatient);
                p.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                p.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                p.Property(e => e.Birthdate).IsRequired();

                p.HasData(
                    new Patient
                    {
                       IdPatient = 1, 
                       FirstName = "Mikołaj", 
                       LastName = "Kozioł", 
                       Birthdate = DateTime.Parse("1999-03-04")
                    },
                    new Patient
                    {
                        IdPatient = 2,
                        FirstName = "Agata",
                        LastName = "Jaruzelska",
                        Birthdate = DateTime.Parse("1979-06-07")
                    }
                    );
            });

            modelBuilder.Entity<Doctor>(d =>
            {
                d.HasKey(e => e.IdDoctor);
                d.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                d.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                d.Property(e => e.Email).IsRequired().HasMaxLength(100);

                d.HasData(
                    new Doctor
                    {
                        IdDoctor = 1,
                        FirstName = "Elizeusz",
                        LastName = "Mariański",
                        Email = "e.marian@medic.pl"
                    },
                    new Doctor
                    {
                        IdDoctor = 2,
                        FirstName = "Janusz",
                        LastName = "Kowalewski",
                        Email = "j.kowalewski@medic.pl"
                    }
                    );
            });

            modelBuilder.Entity<Prescription>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.Property(e => e.Date).IsRequired();
                p.Property(e => e.DueDate).IsRequired();

                p.HasOne(e => e.Patient).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdPatient);
                p.HasOne(e => e.Doctor).WithMany(e => e.Prescriptions).HasForeignKey(e => e.IdDoctor);

                p.HasData(
                    new Prescription
                    {
                        IdPrescription = 1,
                        Date = DateTime.Parse("2022-04-29"),
                        DueDate = DateTime.Parse("2022-05-29"),
                        IdPatient = 1,
                        IdDoctor = 2
                    },
                    new Prescription
                    {
                        IdPrescription = 2,
                        Date = DateTime.Parse("2022-02-13"),
                        DueDate = DateTime.Parse("2022-03-13"),
                        IdPatient = 2,
                        IdDoctor = 1
                    }
                    );
            });

            modelBuilder.Entity<Medicament>(m =>
            {
                m.HasKey(e => e.IdMedicament);
                m.Property(e => e.Name).IsRequired().HasMaxLength(100);
                m.Property(e => e.Description).IsRequired().HasMaxLength(100);
                m.Property(e => e.Type).IsRequired().HasMaxLength(100);

                m.HasData(
                    new Medicament
                    {
                        IdMedicament = 1,
                        Name = "Neperdul",
                        Description = "Over The Counter",
                        Type = "gel"
                    },
                    new Medicament
                    {
                        IdMedicament = 2,
                        Name = "AntiproblemComplex",
                        Description = "Prescription drug",
                        Type = "tabs"
                    }
                    );
            });

            modelBuilder.Entity<Prescription_Medicament>(p =>
            {
                p.HasKey(e => e.IdPrescription);
                p.HasOne(e => e.Prescription).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdPrescription);
                p.HasKey(e => e.IdMedicament);
                p.HasOne(e => e.Medicament).WithMany(e => e.Prescription_Medicaments).HasForeignKey(e => e.IdMedicament);
                p.Property(e => e.Dose);
                p.Property(e => e.Details).IsRequired().HasMaxLength(100);

                p.HasData(
                    new Prescription_Medicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 1,
                        Dose = 1,
                        Details = "Before breakfast"
                    },
                    new Prescription_Medicament
                    {
                        IdMedicament = 1,
                        IdPrescription = 2,
                        Dose = 2,
                        Details = "Before breakfast and supper"
                    },
                    new Prescription_Medicament
                    {
                        IdMedicament = 2,
                        IdPrescription = 2,
                        Dose = 1,
                        Details = "For good morning :)"
                    }

                    );
            });
        }

    }
}
