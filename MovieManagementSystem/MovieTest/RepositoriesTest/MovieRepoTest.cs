using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieManagementSystem.Test.RepositoriesTest
{
    public class MovieRepoTest
    {
        private DbContextOptions<MovieDbContext> _options; // stores configuration options for MovieDbContext ( like receipe)
        private MovieDbContext _context;
        private MovieRepo _movieRepo;

        public MovieRepoTest()
        {
            // STEP1 : set up configuration optons for the in-memory database
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("TestDatabase").Options;

            // STEP2: initialize the in-memory database
            _context = new MovieDbContext(_options);//  creating an empty database
            _context.Database.EnsureDeleted();// make sure that any existing in-memory database is deleted before starting the tests.


            // STEP3: create and add sample movies to the database
                Movie movie1 = new Movie()
                {
                    Title = "Forest Gump"
                };

                Movie movie2 = new Movie()
                {
                    Title = "The Terminal"
                };
                _context.Movies.Add(movie1);
                _context.Movies.Add(movie2);
                _context.SaveChanges();
            
        }

        //STEP4 : Define a test method using xUnit's [Fact] attribute

        [Fact]
        public async Task GetAllShoulReturnListOfMovies_WhenMovieExists()
        {
            // STEP5: Arrange- create objects/Variables/something
       
            _movieRepo = new MovieRepo(_context);

            // STEP6: Act- actions ( try to have some action i.e. call method)
            var result = await _movieRepo.GetAllAsync();

            // STEP 7: Assert - check if the result is as expected
            Assert.NotNull(result);//check if result is not null
            Assert.IsType<List<Movie>>(result);// check if the result is of type List<Movie>
            Assert.Equal(2, result.Count()); // check if the ocunt of movies is 2 as we have inserted 2 objects
        }


        [Fact]
        public async Task GetAllShouldReturnEmptyList_WhenMovieDoesNotExists()
        {
            // Arrange
          
            _context.Movies.RemoveRange(_context.Movies); // remove all the movies from in-memory database
            await _context.SaveChangesAsync();

            _movieRepo = new MovieRepo(_context);

            // Act
            var result = await _movieRepo.GetAllAsync();

            // Assert

            Assert.NotNull(result);// even though the database is empty, it will return non-null or empty list.(don't throw exception)
            Assert.IsType<List<Movie>>(result);//ensures the result is a list even if it's an empty list
            Assert.Empty(result);// check if the result is an empty list

        }


        [Fact]
        public async Task GetByIdShouldReturnIds_WhenIdExists()
        {
            // Arrange
            _movieRepo = new MovieRepo(_context);

            // Act
            var result = await _movieRepo.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result); // check if the result is not null
            Assert.IsType<Movie>(result); // check of the result is of type Movie
            Assert.Equal(1, result.MovieId);// check if the Id matches the expected Id


        }


        [Fact]
        public async Task GetByIdShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            _movieRepo = new MovieRepo(_context);

            // Act
            var result = await _movieRepo.GetByIdAsync(0000); // assuming id:0000 does not exists

            // assert
            Assert.Null(result);// check if the result is null for non-existing ID

        }


        [Fact]
        public async Task DeleteShouldRemoveMovie_WhenMovieExists()
        {
            // Arrange
            _movieRepo = new MovieRepo(_context);

            // Act
            await _movieRepo.DeleteAsync(1);

            // Assert
            var deletedMovie = _context.Movies.Find(1);
            Assert.Null(deletedMovie); // check if the result is null
        }


        [Fact]
        public async Task DeleteShouldNotEffectMovie_WhenMovieDoesNotExists()
        {
            // Arrange
            _movieRepo = new MovieRepo(_context);

            // Act
            await _movieRepo.DeleteAsync(0000); // assuming Id:0000 does not exists

            // Assert
            // Check if the database state remains unchanged (no movies should be deleted)
            var existingMovies = _context.Movies.ToList();
            Assert.Equal(2, existingMovies.Count);
        }


        [Fact]
        public async Task AddShouldCreateNewMovie_WhenValidDataProvided()
        {
            // Arrange
             _movieRepo = new MovieRepo(_context);
            var movie = new Movie { Title = "Inception" };

            // Act
            var result = await _movieRepo.CreateAsync(movie);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(movie.Title, result.Title);// if the mvoie is equal to result or not? i.e if movie = "Inception" or not?

            // check if the movie is added 
            Assert.Equal(3, _context.Movies.Count());


        }


        [Fact]
        public async Task UpdateShouldModifyExistingMovie_WhenValidDataProvided()
        {
            // Arrange
            _movieRepo = new MovieRepo(_context);

            // Act
            Movie existingMovie = await _movieRepo.GetByIdAsync(1);

            if (existingMovie != null) // if award with the specified ID is found in the repo
            {
                // Modify the existing Award
                existingMovie.Title = "Updated Movie";

                // Update the Award in the repository
                await _movieRepo.UpdateAsync(existingMovie);

                // Assert
                var updatedAward = await _movieRepo.GetByIdAsync(1);

                Assert.NotNull(updatedAward);
                Assert.Equal("Updated Award", updatedAward.Title);
            }
            else
            {
                Assert.True(false, "Movie with the specified Id does not exist.");
            }


        }


    }
}
