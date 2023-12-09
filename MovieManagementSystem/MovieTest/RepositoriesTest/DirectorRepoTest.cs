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
    public class DirectorRepoTest
    {
        private DbContextOptions<MovieDbContext> _options; // stores ocnfiguration options for MovieDbContext
        private MovieDbContext _context;
        private DirectorRepo _directorRepo;

        public DirectorRepoTest()
        {
            // set up configuration options for the in-memory database
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("TestDatabase").Options;

            // initialize the in-memory database
            _context = new MovieDbContext(_options);
            _context.Database.EnsureDeleted();

            // create and add sample data

            Director director1 = new Director { Name = "Director1" };
            Director director2 = new Director { Name = "Director2" };

            _context.Directors.Add(director1);
            _context.Directors.Add(director2);
            _context.SaveChanges();
        }

        // define a test method using xUnit's [Fact] attribute
        [Fact]
        public async Task GetAllShouldReturnListOfDirectors_WhenDirectorExists()
        {
            // Arrange
            _directorRepo = new DirectorRepo(_context);

            // Act
            var result = await _directorRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Director>>(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetAllShouldReturnEmptyList_WhenDirectorDoesNotExists()
        {
            // Arrange
            _context.Directors.RemoveRange(_context.Directors);
            await _context.SaveChangesAsync();
            _directorRepo = new DirectorRepo(_context);

            // Act
            var result = await _directorRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result); // even though the database is empty, it will return non-null or empty list( don't throw exception)
            Assert.IsType<List<Director>>(result); // ensures the result is a list even if its an empty list
            Assert.Empty(result); //

        }

        [Fact]
        public async Task GetByIdShouldReturnListOfDirectors_WhenDirectorExists()
        {
            // Arrange
            _directorRepo = new DirectorRepo(_context);

            // Act
            var result = await _directorRepo.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Director>(result);
            Assert.Equal(1, result.DirectorId);

        }

        [Fact]
        public async Task GetByIdShouldReturnNull_WhenDirectorDoesNotExists()
        {
            // Arrange
            _directorRepo = new DirectorRepo(_context);

            // Act
            var result = await _directorRepo.GetByIdAsync(0000); // assuming id: 0000 does not exists

            // Assert
            Assert.Null(result); // check if the result is null for non-exsting ID

        }

        [Fact]
        public async Task DeleteShouldRemoveDirector_WhenDirectorExists()
        {

            // Arrange
            _directorRepo = new DirectorRepo(_context);

            // Act
            await _directorRepo.DeleteAsync(1);

            // Assert
            var deletedDirector = _context.Directors.Find(1);
            Assert.Null(deletedDirector); // check if the result isnull
        }

        [Fact]
        public async Task DeleteShouldNotEffectDirector_WhenDirectorDoesNotExists()
        {
            // Arrange
            _directorRepo = new DirectorRepo(_context);

            // Act
            await _directorRepo.DeleteAsync(0000);

            // assert 
            // check if the databse state remains unchanged (no moives should be deleted)
            var existingDirectors = _context.Directors.ToList();
            Assert.Equal(2, existingDirectors.Count);

        }

        [Fact]
        public async Task AddShouldCreateNewDirector_WhenValidDataProvided()
        {

            // Arrange
            _directorRepo = new DirectorRepo(_context);
            var director = new Director { Name = "Director3" };

            // Act
            var result = await _directorRepo.CreateAsync(director);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(director, result);// if the mvoie is equal to result or not? i.e if movie = "Inception" or not?

            // check if the movie is added 
            Assert.Equal(3, _context.Directors.Count());

        }

        [Fact]
        public async Task UpdateShouldModifyExistingDirector_WhenValidDataProvided()
        {
            // Arrange
            _directorRepo = new DirectorRepo(_context);

            Director existingDirector = await _directorRepo.GetByIdAsync(1);// assuming Id: 0000 does not exists

            if (existingDirector != null)
            {
                // Act
                existingDirector.Name = "Updated Director";
                var result = await _directorRepo.UpdateAsync(existingDirector);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(existingDirector.DirectorId, result.DirectorId);
                Assert.Equal("Updated Director", result.Name);

            }
            else
            {
                Assert.True(false,"Director with the specified Id does not exists");
            }
            

        }



    }
}
