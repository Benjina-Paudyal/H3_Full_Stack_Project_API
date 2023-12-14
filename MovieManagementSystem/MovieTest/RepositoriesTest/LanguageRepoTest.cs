using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieManagementSystem.Test.RepositoriesTest
{
    public class LanguageRepoTest
    {
        private DbContextOptions<MovieDbContext> _options; // stores ocnfiguration options for MovieDbContext
        private MovieDbContext _context;
        private LanguageRepo _languageRepo;

        public LanguageRepoTest()
        {
            // set up configuration options for the in-memory database
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("TestDatabase").Options;

            // initialize the in-memory database
            _context = new MovieDbContext(_options);
            _context.Database.EnsureDeleted();

            // create and add sample data

            Language language1 = new Language { Name = "English" };
            Language language2 = new Language { Name = "Danish" };

            _context.Languages.Add(language1);
            _context.Languages.Add(language2);
            _context.SaveChanges();
        }

        // define a test method using xUnit's [Fact] attribute
        [Fact]
        public async Task GetAllShouldReturnListOfLanguages_WhenLanguageExists()
        {
            // Arrange
            _languageRepo = new LanguageRepo(_context);

            // Act
            var result = await _languageRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Language>>(result);
            Assert.Equal(2, result.Count());

        }

        [Fact]
        public async Task GetAllShouldReturnEmptyList_WhenLanguageDoesNotExists()
        {
            // Arrange
            _context.Languages.RemoveRange(_context.Languages);
            await _context.SaveChangesAsync();
            _languageRepo = new LanguageRepo(_context);

            // Act
            var result = await _languageRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result); // even though the database is empty, it will return non-null or empty list( don't throw exception)
            Assert.IsType<List<Language>>(result); // ensures the result is a list even if its an empty list
            Assert.Empty(result); //

        }

        [Fact]
        public async Task GetByIdShouldReturnListOfLanguages_WhenLanguageExists()
        {
            // Arrange
            _languageRepo = new LanguageRepo(_context);

            // Act
            var result = await _languageRepo.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Language>(result);
            Assert.Equal(1, result.LanguageId);

        }

        [Fact]
        public async Task GetByIdShouldReturnNull_WhenLanguageDoesNotExists()
        {
            // Arrange
            _languageRepo = new LanguageRepo(_context);

            // Act
            var result = await _languageRepo.GetByIdAsync(0000); // assuming id: 0000 does not exists

            // Assert
            Assert.Null(result); // check if the result is null for non-exsting ID

        }

        [Fact]
        public async Task DeleteShouldRemoveLanguage_WhenLangugaeExists()
        {

            // Arrange
            _languageRepo = new LanguageRepo(_context);

            // Act
            await _languageRepo.DeleteAsync(1);

            // Assert
            var deletedLanguage = _context.Languages.Find(1);
            Assert.Null(deletedLanguage); // check if the result isnull
        }

        [Fact]
        public async Task DeleteShouldNotEffectLanguage_WhenLanguageDoesNotExists()
        {
            // Arrange
            _languageRepo = new LanguageRepo(_context);

            // Act
            await _languageRepo.DeleteAsync(0000);

            // assert 
            // check if the databse state remains unchanged (no moives should be deleted)
            var existingLanguages = _context.Languages.ToList();
            Assert.Equal(2, existingLanguages.Count);

        }

        [Fact]
        public async Task AddShouldCreateNewLanguage_WhenValidDataProvided()
        {

            // Arrange
            _languageRepo = new LanguageRepo(_context);
            var language = new Language { Name = "Nepali" };

            // Act
            var result = await _languageRepo.CreateAsync(language);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(language, result);// if the mvoie is equal to result or not? i.e if movie = "Inception" or not?

            // check if the movie is added 
            Assert.Equal(3, _context.Languages.Count());

        }

        [Fact]
        public async Task UpdateShouldModifyExistingLanguage_WhenValidDataProvided()
        {
            // Arrange
            _languageRepo = new LanguageRepo(_context);

            Language existingLanguage = await _languageRepo.GetByIdAsync(1);// assuming Id: 0000 does not exists

            if (existingLanguage != null)
            {
                // Act
                existingLanguage.Name = "Updated Language";
                var result = await _languageRepo.UpdateAsync(existingLanguage);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(existingLanguage.LanguageId, result.LanguageId);
                Assert.Equal("Updated Language", result.Name);

            }
            else
            {
                Assert.True(false, "Language with the specified Id does not exists");
            }


        }

    }
}
