// <auto-generated />
using System;
using EcoRoute.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcoRoute.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("EcoRoute.Infrastructure.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Latitude")
                        .HasColumnType("TEXT");

                    b.Property<string>("Longitude")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("EcoRoute.Infrastructure.Models.SensorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Aqi")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("TEXT");

                    b.Property<float>("Co2")
                        .HasColumnType("REAL");

                    b.Property<float>("DustPm1")
                        .HasColumnType("REAL");

                    b.Property<float>("DustPm10")
                        .HasColumnType("REAL");

                    b.Property<float>("DustPm25")
                        .HasColumnType("REAL");

                    b.Property<float>("Formaldehyde")
                        .HasColumnType("REAL");

                    b.Property<float>("Humidity")
                        .HasColumnType("REAL");

                    b.Property<float>("Los")
                        .HasColumnType("REAL");

                    b.Property<float>("Pressure")
                        .HasColumnType("REAL");

                    b.Property<int?>("SensorId")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Temperature")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SensorDataList");
                });

            modelBuilder.Entity("EcoRoute.Infrastructure.Models.SensorData", b =>
                {
                    b.HasOne("EcoRoute.Infrastructure.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId");

                    b.Navigation("Sensor");
                });
#pragma warning restore 612, 618
        }
    }
}
