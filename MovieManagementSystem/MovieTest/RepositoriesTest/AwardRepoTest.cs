using Xunit;
using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using System.Collections.Generic;
using MovieManagementSystem.API.Data.Domain;

namespace MovieManagementSystem.Test.RepositoriesTest
{
    /*

 -> DbContextOptions: represents the configuration options for a 'DbContext' which is provided to the 
    constructor of DbContext when creating an instance.
 -> DbContextOptionsBuilder is a helper class  for building and configuring instances of 'DbContextOptions' 
    and often used in testing scenarios to set up specific configurations like in-memory databases.It provides
    methods for configuing various options, such as the database provider, connection string, and other runtime behavior.
    It is used to configure the options before building the final DbContextOptions.
 -> .UseInMemoryDatabase ("TestDatabase): configures the options to use an in-memory database. This 
    in memeory dataabses are used to isolate the tests from a real database and the data is stored in 
    memory rather than a persistent storage medium
 -> .Options: finalizes the configuration and produces the actual 'DbContextOptions<MovieDbContext> object. The Options property is where we
     get the configured options ready for use with DbContext
-> _options: finally the value is assigned the configured options.This field is used later for creating an instance of MovieDbContext

    */

    public class AwardRepoTest
    {
        private DbContextOptions<MovieDbContext> _options; // stores configuration options for MovieDbContext
        private MovieDbContext _context;
        private AwardRepo _awardRepo;
        public AwardRepoTest()
        {
            // STEP 1: set up configuration options for the in-memory database
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("TestDatabase").Options;

            // STEP 2: initialize the in-memory database 
            _context = new MovieDbContext(_options); // creating an empty database
            _context.Database.EnsureDeleted(); // make sure that any existing in-memory database is deleted before starting the tests.

            // STEP 3: Create and add some sample awards and movies to the database
            Award award1 = new Award()
            {
                AwardName = "Best Actor Male",
                MovieId = 1,
                Movie = new Movie { MovieId = 1, Title = "Movie1" }
            };

            Award award2 = new Award()
            {
                AwardName = "Best Actor Female",
                MovieId = 2,
                Movie = new Movie { MovieId = 2, Title = "Movie2" }
            };

            Award award3 = new Award()
            {
                AwardName = "Best Director",
                MovieId = 3,
                Movie = new Movie { MovieId = 3, Title = "Movie3" }
            };
            _context.Awards.Add(award1);
            _context.Awards.Add(award2);
            _context.Awards.Add(award3);
            _context.SaveChanges();
        }


        // STEP 4: Define a test method using xUnit's [Fact] attribute
        [Fact]
        public async Task GetAllShouldReturnListOfAwards_WhenAwardExists()
        {
            // STEP 5 :Arrange - create objects/varibales/something

            _awardRepo = new AwardRepo(_context);

            // STEP 6: Act - actions (try to have some action i.e. call method)
            var result = await _awardRepo.GetAllAsync();

            // STEP 7: Assert - check if the result is as expected
            Assert.NotNull(result); // check if the result is not null
            Assert.IsType<List<Award>>(result); // check if the result is of type List<Award>
            Assert.Equal(3, result.Count()); // check if the count of awards is 3 as we have inserted 3 objects ( Count() for Enumerable)

        }


        [Fact]
        public async Task GetAllShouldReturnEmptyList_WhenAwardDoesNotExists()
        {
            // Arrange
            _context.Awards.RemoveRange(_context.Awards); // Remove all awards from in-memory database

            await _context.SaveChangesAsync();

            _awardRepo = new AwardRepo(_context);

            // Act
            var result = await _awardRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result); // even though the database is empty, it will return non - null or empty list.(don't throw exception)
            Assert.IsType<List<Award>>(result); // ensures the result is a list even if it's an empty list
            Assert.Empty(result); // Check if the result is an empty list
        }


        [Fact]
        public async Task GetByIdShouldReturnIds_WhenIdExists()
        {
            // STEP 5 :Arrange - create objects/varibales/something

            _awardRepo = new AwardRepo(_context);

            // STEP 6: Act - actions (try to have some action i.e. call method)
            var result = await _awardRepo.GetByIdAsync(2);

            // STEP 7: Assert - check if the result is as expected
            Assert.NotNull(result); // check if the result is not null
            Assert.IsType<Award>(result); // check if the result is of type Award
            Assert.Equal(2, result.AwardId); // check if the Id matches the expected Id
            Assert.Equal("Best Actor Female", result.AwardName); // check if the name matches the expected name

        }


        [Fact]
        public async Task GetByIdShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _awardRepo = new AwardRepo(_context);

            // Act
            var result = await _awardRepo.GetByIdAsync(0000); // Assuming 0000 is a non-existing ID

            // Assert
            Assert.Null(result); // Check if the result is null for non-existing ID
        }


        [Fact]
        public async Task DeleteShouldRemoveAward_WhenAwardExists()
        {
            // STEP 5 :Arrange - create objects/varibales/something

            _awardRepo = new AwardRepo(_context);
            int AwardIdToDelete = 1; // Id of the Award to delete

            // STEP 6: Act - actions (try to have some action i.e. call method)
            await _awardRepo.DeleteAsync(AwardIdToDelete);

            // STEP 7: Assert - check if the result is as expected
            var deletedAward = _context.Awards.Find(AwardIdToDelete);
            Assert.Null(deletedAward); // check if the result is null


        }


        [Fact]
        public async Task DeleteShouldNotAffectAward_WhenAwardDoesNotExists()
        {
            // Arrange

            _awardRepo = new AwardRepo(_context);

            // Act
            await _awardRepo.DeleteAsync(0000);// Assuming 0000 is a non-existing ID

            // Assert
            // Check if the database state remains unchanged (no awards should be deleted)
            var existingAwards = _context.Awards.ToList();
            Assert.Equal(3, existingAwards.Count);

        }


        [Fact]
        public async Task AddShouldCreateNewAward_WhenValidDataProvided()
        {

            // Arrange

            _awardRepo = new AwardRepo(_context);

            int existingMovieId = 1;

            // Load the existing Movie from the context
            var existingMovie = _context.Movies.Find(existingMovieId);

            if (existingMovie != null)
            {
                // Update the existing Movie to be tracked by the context
                _context.Update(existingMovie);

                // Create new Award object associated with the existing Movie
                Award award = new Award()
                {
                    AwardName = "Best child artist",
                    MovieId = existingMovieId,
                    Movie = existingMovie
                };

                // Act
                await _awardRepo.CreateAsync(award);

                // Assert
                var newAward = _context.Awards
                    .Include(a => a.Movie)
                    .FirstOrDefault(a => a.AwardName == "Best child artist");

                Assert.NotNull(newAward);
                Assert.NotNull(newAward.Movie);
                Assert.Equal("Best child artist", newAward.AwardName);
                Assert.Equal(existingMovieId, newAward.Movie.MovieId);
            }
            else
            {
                return; // skip the test
            }
        }


        [Fact]
        public async Task UpdateShouldModifyExistingAward_WhenValidDataProvided()
        {
            // Arrange
            _awardRepo = new AwardRepo(_context);

            // Act
            Award existingAward = await _awardRepo.GetByIdAsync(1);

            if (existingAward != null) // if award with the specified ID is found in the repo
            {
                // Modify the existing Award
                existingAward.AwardName = "Updated Award";

                // Update the Award in the repository
                await _awardRepo.UpdateAsync(existingAward);

                // Assert
                var updatedAward = await _awardRepo.GetByIdAsync(1);

                Assert.NotNull(updatedAward);
                Assert.Equal("Updated Award", updatedAward.AwardName);
            }
            else
            {
                Assert.True(false, "Award with the specified Id does not exist.");
            }
        }

    }
}



