using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Data
{
    public class MovieDbContext : DbContext
    {
        //Constructor is uesd to pass DbContextOptions to the base class DbContext 
        // It provides connection information to the database

        // Hey when you create a new MovieDbContext , make sure to give it the necessary instructions about the database.
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
           
        }
        // DbSet properties represent tables in the databse which will be used to interact with corresponding entities

        public DbSet<Actor> Actors { get; set; } // DbSet is a collection which hold entities of the type 'Actor'

        public DbSet<Award> Awards { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seeding data for the domain models

            // Actor
            modelBuilder.Entity<Actor>().HasData
                (
                new Actor { ActorId = 1, Name = "Actor1" },
                new Actor { ActorId = 2, Name = "Actor2" }
                );

            // Director
            modelBuilder.Entity<Director>().HasData
                (
                new Director { DirectorId = 1, Name = "Director1" },
                new Director { DirectorId = 2, Name = "Director2" }
                );

            // Language
            modelBuilder.Entity<Language>().HasData
                (
                new Language { LanguageId = 1, Name = "Language1" },
                new Language { LanguageId = 2, Name = "Language2" }
                );

            // Country
            modelBuilder.Entity<Country>().HasData
                (
                new Country { CountryId = 1, Name = "Country1" },
                new Country { CountryId = 2, Name = "Country2" }
                );

            // Movie
            modelBuilder.Entity<Movie>().HasData
                (
                new Movie { MovieId = 1, Title = "Movie1", DirectorId = 1, CountryId = 1, LanguageId = 1 },
                new Movie { MovieId = 2, Title = "Movie2", DirectorId = 1, CountryId = 1, LanguageId = 2 },
                new Movie { MovieId = 3, Title = "Movie3", DirectorId = 2, CountryId = 2, LanguageId = 1 }
                );

            // Award
            modelBuilder.Entity<Award>().HasData
                (
                 new Award { AwardId = 1, AwardName = "Award1", MovieId = 1 },
                 new Award { AwardId = 2, AwardName = "Award2", MovieId = 2 }
                );

            // Genre
            modelBuilder.Entity<Genre>().HasData(
                new Genre { GenreId = 1, Name = "Genre1" },
                new Genre { GenreId = 2, Name = "Genre2" }
            );

            


        }
    }
        


}
