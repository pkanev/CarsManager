﻿// <auto-generated />
using System;
using CarsManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CarsManager.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CarsManager.Domain.Entities.CarInsurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("CarInsurances");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.CivilLiability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("CivilLiabilities");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("GivenName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<bool?>("IsEmployed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PostCode")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Telephone")
                        .HasColumnType("text");

                    b.Property<int>("TownId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TownId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.MOT", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("MOTs");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Make", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Makes");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("MakeId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("VehicleType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MakeId", "Name")
                        .IsUnique();

                    b.ToTable("Models");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<decimal>("FinalPrice")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsBatteryChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBeltChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBrakeDisksChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsBrakeLiningsChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCoolantChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFuelFilterChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOilChanged")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOtherWorkDone")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSparkPlugChanged")
                        .HasColumnType("boolean");

                    b.Property<int>("Mileage")
                        .HasColumnType("integer");

                    b.Property<int>("RepairShopId")
                        .HasColumnType("integer");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RepairShopId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Repairs");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.RepairShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("RepairShops");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.RoadBookEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime?>("CheckedIn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CheckedOut")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Destination")
                        .HasColumnType("text");

                    b.Property<int?>("NewMileage")
                        .HasColumnType("integer");

                    b.Property<int>("OldMileage")
                        .HasColumnType("integer");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("RoadBookEntries");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Town", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("BeltMileage")
                        .HasColumnType("integer");

                    b.Property<int>("BrakeDisksMileage")
                        .HasColumnType("integer");

                    b.Property<int>("BrakeLiningsMileage")
                        .HasColumnType("integer");

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<int>("CoolantMileage")
                        .HasColumnType("integer");

                    b.Property<int>("EngineDisplacement")
                        .HasColumnType("integer");

                    b.Property<DateTime>("FirstRegistration")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Fuel")
                        .HasColumnType("integer");

                    b.Property<int>("FuelConsumption")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("boolean");

                    b.Property<string>("LicencePlate")
                        .HasColumnType("text");

                    b.Property<int>("Mileage")
                        .HasColumnType("integer");

                    b.Property<int>("ModelId")
                        .HasColumnType("integer");

                    b.Property<int>("OilMileage")
                        .HasColumnType("integer");

                    b.Property<int?>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LicencePlate")
                        .IsUnique();

                    b.HasIndex("ModelId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Vignette", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("VehicleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Vignettes");
                });

            modelBuilder.Entity("EmployeeRoadBookEntry", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("integer");

                    b.Property<int>("RoadBookEntriesId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeesId", "RoadBookEntriesId");

                    b.HasIndex("RoadBookEntriesId");

                    b.ToTable("EmployeeRoadBookEntry");
                });

            modelBuilder.Entity("EmployeeRoadBookEntry1", b =>
                {
                    b.Property<int>("ActiveRecordsId")
                        .HasColumnType("integer");

                    b.Property<int>("ActiveUsersId")
                        .HasColumnType("integer");

                    b.HasKey("ActiveRecordsId", "ActiveUsersId");

                    b.HasIndex("ActiveUsersId");

                    b.ToTable("EmployeeRoadBookEntry1");
                });

            modelBuilder.Entity("EmployeeVehicle", b =>
                {
                    b.Property<int>("EmployeesId")
                        .HasColumnType("integer");

                    b.Property<int>("VehiclesId")
                        .HasColumnType("integer");

                    b.HasKey("EmployeesId", "VehiclesId");

                    b.HasIndex("VehiclesId");

                    b.ToTable("EmployeeVehicle");
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.Property<string>("RolesName")
                        .HasColumnType("text");

                    b.Property<string>("UsersId")
                        .HasColumnType("text");

                    b.HasKey("RolesName", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserUserRole");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.CarInsurance", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("CarInsurances")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.CivilLiability", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("CivilLiabilities")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Employee", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Town", "Town")
                        .WithMany()
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Town");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.MOT", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("MOTs")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Model", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Make", "Make")
                        .WithMany("Models")
                        .HasForeignKey("MakeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Make");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Repair", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.RepairShop", "RepairShop")
                        .WithMany("Repairs")
                        .HasForeignKey("RepairShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("Repairs")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RepairShop");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.RoadBookEntry", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("RoadBookEntries")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Vehicle", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Vignette", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany("Vignettes")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("EmployeeRoadBookEntry", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsManager.Domain.Entities.RoadBookEntry", null)
                        .WithMany()
                        .HasForeignKey("RoadBookEntriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeRoadBookEntry1", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.RoadBookEntry", null)
                        .WithMany()
                        .HasForeignKey("ActiveRecordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsManager.Domain.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("ActiveUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EmployeeVehicle", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsManager.Domain.Entities.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehiclesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserUserRole", b =>
                {
                    b.HasOne("CarsManager.Domain.Entities.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RolesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarsManager.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Make", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.RepairShop", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("CarsManager.Domain.Entities.Vehicle", b =>
                {
                    b.Navigation("CarInsurances");

                    b.Navigation("CivilLiabilities");

                    b.Navigation("MOTs");

                    b.Navigation("Repairs");

                    b.Navigation("RoadBookEntries");

                    b.Navigation("Vignettes");
                });
#pragma warning restore 612, 618
        }
    }
}
