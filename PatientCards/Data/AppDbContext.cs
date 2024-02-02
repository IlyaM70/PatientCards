using Microsoft.EntityFrameworkCore;
using PatientCards.Models;

namespace PatientCards.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Один ко многим Пациент и Визиты
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .IsRequired();

            // Заполнить данные
            #region Patients
            modelBuilder.Entity<Patient>().HasData(
                    new Patient()
                    {
                        Id = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        LastName = "Морозов",
                        FirstName = "Александр",
                        Patronymic = "Васильевич",
                        DateOfBirth = new DateOnly(1976,10,11),
                        Phone = "+79171223345"
                    },
                    new Patient()
                    {
                        Id = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        LastName = "Романов",
                        FirstName = "Петр",
                        Patronymic = "",
                        DateOfBirth = new DateOnly(1986, 9, 9),
                        Phone = "+79191223385"
                    }
                );
            #endregion
            #region Appointments
            modelBuilder.Entity<Appointment>().HasData(
                    new Appointment()
                    {
                        Id = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        PatientId = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        DateOfAppointment = DateTime.UtcNow.AddDays(-10),
                        Diagnosis = "E00.0,Синдром врожденной йодной недостаточности, неврологическая форма",
                    },
                    new Appointment()
                    {
                        Id = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        PatientId = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        DateOfAppointment = DateTime.UtcNow,
                        Diagnosis = "E01.0,Диффузный (эндемический) зоб, связанный с йодной недостаточностью",
                    },
                    new Appointment()
                    {
                        Id = "12FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        PatientId = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        DateOfAppointment = DateTime.UtcNow.AddDays(-100),
                        Diagnosis = "E10,Сахарный диабет I типа",
                    },
                    new Appointment()
                    {
                        Id = "13FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        PatientId = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                        DateOfAppointment = DateTime.UtcNow.AddDays(-1),
                        Diagnosis = "E11,Сахарный диабет II типа",
                    }
                );
            #endregion
        }
    }
}
