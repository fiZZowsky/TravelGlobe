﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelGlobe.Infrastructure.Persistance;

#nullable disable

namespace TravelGlobe.Infrastructure.Migrations
{
    [DbContext(typeof(TravelGlobeDbContext))]
    partial class TravelGlobeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.18");

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Country", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArrivalAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartureAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DistanceKm")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReturnArrivalAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReturnDepartureAirportId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ArrivalAirportId");

                    b.HasIndex("CountryCode");

                    b.HasIndex("DepartureAirportId");

                    b.HasIndex("ReturnArrivalAirportId");

                    b.HasIndex("ReturnDepartureAirportId");

                    b.HasIndex("UserId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Airport", b =>
                {
                    b.OwnsOne("TravelGlobe.Domain.ValueObjects.AirportCode", "Code", b1 =>
                        {
                            b1.Property<int>("AirportId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Iata")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("TEXT")
                                .HasColumnName("Iata");

                            b1.HasKey("AirportId");

                            b1.ToTable("Airports");

                            b1.WithOwner()
                                .HasForeignKey("AirportId");
                        });

                    b.OwnsOne("TravelGlobe.Domain.ValueObjects.GeoCoordinate", "Location", b1 =>
                        {
                            b1.Property<int>("AirportId")
                                .HasColumnType("INTEGER");

                            b1.Property<double>("Latitude")
                                .HasColumnType("REAL")
                                .HasColumnName("Latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("REAL")
                                .HasColumnName("Longitude");

                            b1.HasKey("AirportId");

                            b1.ToTable("Airports");

                            b1.WithOwner()
                                .HasForeignKey("AirportId");
                        });

                    b.Navigation("Code")
                        .IsRequired();

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Trip", b =>
                {
                    b.HasOne("TravelGlobe.Domain.Entities.Airport", null)
                        .WithMany()
                        .HasForeignKey("ArrivalAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelGlobe.Domain.Entities.Country", null)
                        .WithMany("Trips")
                        .HasForeignKey("CountryCode")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelGlobe.Domain.Entities.Airport", null)
                        .WithMany()
                        .HasForeignKey("DepartureAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelGlobe.Domain.Entities.Airport", null)
                        .WithMany()
                        .HasForeignKey("ReturnArrivalAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelGlobe.Domain.Entities.Airport", null)
                        .WithMany()
                        .HasForeignKey("ReturnDepartureAirportId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TravelGlobe.Domain.Entities.User", null)
                        .WithMany("Trips")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.Country", b =>
                {
                    b.Navigation("Trips");
                });

            modelBuilder.Entity("TravelGlobe.Domain.Entities.User", b =>
                {
                    b.Navigation("Trips");
                });
#pragma warning restore 612, 618
        }
    }
}
