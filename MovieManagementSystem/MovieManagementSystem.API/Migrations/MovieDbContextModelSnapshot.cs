﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieManagementSystem.API.Data;

#nullable disable

namespace MovieManagementSystem.API.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    partial class MovieDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.24")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Actor", b =>
                {
                    b.Property<int>("ActorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ActorId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ActorId");

                    b.ToTable("Actors");

                    b.HasData(
                        new
                        {
                            ActorId = 1,
                            Name = "Actor1"
                        },
                        new
                        {
                            ActorId = 2,
                            Name = "Actor2"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Award", b =>
                {
                    b.Property<int>("AwardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AwardId"), 1L, 1);

                    b.Property<string>("AwardName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("AwardId");

                    b.HasIndex("MovieId");

                    b.ToTable("Awards");

                    b.HasData(
                        new
                        {
                            AwardId = 1,
                            AwardName = "Award1",
                            MovieId = 1
                        },
                        new
                        {
                            AwardId = 2,
                            AwardName = "Award2",
                            MovieId = 2
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryId = 1,
                            Name = "Country1"
                        },
                        new
                        {
                            CountryId = 2,
                            Name = "Country2"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Director", b =>
                {
                    b.Property<int>("DirectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DirectorId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DirectorId");

                    b.ToTable("Directors");

                    b.HasData(
                        new
                        {
                            DirectorId = 1,
                            Name = "Director1"
                        },
                        new
                        {
                            DirectorId = 2,
                            Name = "Director2"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Name = "Genre1"
                        },
                        new
                        {
                            GenreId = 2,
                            Name = "Genre2"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LanguageId"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            LanguageId = 1,
                            Name = "Language1"
                        },
                        new
                        {
                            LanguageId = 2,
                            Name = "Language2"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("DirectorId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MovieId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DirectorId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            MovieId = 1,
                            CountryId = 1,
                            DirectorId = 1,
                            LanguageId = 1,
                            Title = "Movie1"
                        },
                        new
                        {
                            MovieId = 2,
                            CountryId = 1,
                            DirectorId = 1,
                            LanguageId = 2,
                            Title = "Movie2"
                        },
                        new
                        {
                            MovieId = 3,
                            CountryId = 2,
                            DirectorId = 2,
                            LanguageId = 1,
                            Title = "Movie3"
                        });
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.MovieActor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ActorId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieActors");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.MovieGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("MovieId");

                    b.ToTable("MovieGenres");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Award", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Movie", "Movie")
                        .WithMany("Awards")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Booking", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.User", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Movie", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Country", "Country")
                        .WithMany("MoviesProduced")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.Director", "Director")
                        .WithMany("MovieDirectors")
                        .HasForeignKey("DirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.Language", "Language")
                        .WithMany("Movies")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Director");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.MovieActor", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Actor", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.MovieGenre", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.Movie", "Movie")
                        .WithMany("MoviesGenres")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Review", b =>
                {
                    b.HasOne("MovieManagementSystem.API.Models.Domain.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieManagementSystem.API.Models.Domain.User", null)
                        .WithMany("Reviews")
                        .HasForeignKey("UserId");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Actor", b =>
                {
                    b.Navigation("MovieActors");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Country", b =>
                {
                    b.Navigation("MoviesProduced");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Director", b =>
                {
                    b.Navigation("MovieDirectors");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Language", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.Movie", b =>
                {
                    b.Navigation("Awards");

                    b.Navigation("MoviesGenres");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieManagementSystem.API.Models.Domain.User", b =>
                {
                    b.Navigation("Bookings");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
