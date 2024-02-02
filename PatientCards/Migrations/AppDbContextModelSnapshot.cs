﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PatientCards.Data;

#nullable disable

namespace PatientCards.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PatientCards.Models.Appointment", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateTime>("DateOfAppointment")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnosis")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PatientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfAppointment = new DateTime(2024, 1, 21, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1596),
                            Diagnosis = "E00.0,Синдром врожденной йодной недостаточности, неврологическая форма",
                            PatientId = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE"
                        },
                        new
                        {
                            Id = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfAppointment = new DateTime(2024, 1, 31, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1606),
                            Diagnosis = "E01.0,Диффузный (эндемический) зоб, связанный с йодной недостаточностью",
                            PatientId = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE"
                        },
                        new
                        {
                            Id = "12FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfAppointment = new DateTime(2023, 10, 23, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1608),
                            Diagnosis = "E10,Сахарный диабет I типа",
                            PatientId = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE"
                        },
                        new
                        {
                            Id = "13FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfAppointment = new DateTime(2024, 1, 30, 10, 49, 32, 56, DateTimeKind.Utc).AddTicks(1611),
                            Diagnosis = "E11,Сахарный диабет II типа",
                            PatientId = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE"
                        });
                });

            modelBuilder.Entity("PatientCards.Models.Patient", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            Id = "10FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfBirth = new DateOnly(1976, 10, 11),
                            FirstName = "Александр",
                            LastName = "Морозов",
                            Patronymic = "Васильевич",
                            Phone = "+79171223345"
                        },
                        new
                        {
                            Id = "11FD2873-FF9D-4951-9346-1C71BD0AC0DE",
                            DateOfBirth = new DateOnly(1986, 9, 9),
                            FirstName = "Петр",
                            LastName = "Романов",
                            Patronymic = "",
                            Phone = "+79191223385"
                        });
                });

            modelBuilder.Entity("PatientCards.Models.Appointment", b =>
                {
                    b.HasOne("PatientCards.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PatientCards.Models.Patient", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}