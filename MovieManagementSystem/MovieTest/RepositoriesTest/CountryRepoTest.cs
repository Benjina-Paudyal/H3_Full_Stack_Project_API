using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using MovieManagementSystem.API.Repositories.Interfaces;
using MovieManagementSystem.API.Data.Domain;

namespace MovieManagementSystem.Test.RepositoriesTest
{


    public class CountryRepoTest
    {
        private DbContextOptions<MovieDbContext> _options; // stores configuration options for MovieDbContext
        private MovieDbContext _context;
        private CountryRepo _countryRepo;

        public CountryRepoTest()
        {
            // set up configuration options for the in-memory database
            _options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("TestDatabase").Options;

            // initialize the in-memory database
            _context = new MovieDbContext(_options);
            _context.Database.EnsureDeleted(); // make sure that any existing in-memory database is deleted before starting the tests.

            // create and add sample countries to the database
            Country country1 = new Country { Name = "Nepal" };
            Country country2 = new Country { Name = "Denmark" };

            _context.Countries.Add(country1);
            _context.Countries.Add(country2);
            _context.SaveChanges();
        }

        // Define a test method using xUnit's [Fact] attribute

        [Fact]
        public async Task GetALlShouldReturnListOfCountries_WhenCountryExists()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act 
            var result = await _countryRepo.GetAllAsync();

            // Assert
            Assert.NotNull(result); // check is result is not null
            Assert.IsType<List<Country>>(result); // check if the result is of type List<Country>
            Assert.Equal(2, result.Count()); // check if the count of countries is 2 as we have inserted 2 objects


        }

        [Fact]
        public async Task GetAllShouldReturnEmptyList_WhenCountryDoesNotExists()
        {
            // Arrange
            _context.Countries.RemoveRange(_context.Countries); // remove all the countries from in-memory database
            await _context.SaveChangesAsync();
            _countryRepo = new CountryRepo(_context);

            // Act
            var result = await _countryRepo.GetAllAsync();

            // Assert

            Assert.NotNull(result); // even though the database is empty, it will return non-null or empty list( don't throw exception)
            Assert.IsType<List<Country>>(result); // ensures the result is a list even if it's an empty list
            Assert.Empty(result); // check if the result is an empty list

        }

        [Fact]
        public async Task GetByIdShouldReturnIds_WhenIdExists()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act
            var result = await _countryRepo.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Country>(result);
            Assert.Equal(1, result.CountryId);


        }

        [Fact]
        public async Task GetByIdShouldReturnNull_WhenIdDoesNotExists()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act
            var result = await _countryRepo.GetByIdAsync(0000); // assuming id: 0000 does not exists

            // Assert
            Assert.Null(result); // check if the result is null for non-exsting ID

        }

        [Fact]
        public async Task DeleteShouldRemoveCountry_WhenCountryExists()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act
            await _countryRepo.DeleteAsync(1);

            // Assert
            var deletedCountry = _context.Countries.Find(1);
            Assert.Null(deletedCountry); // check if the result isnull
        }

        [Fact]
        public async Task DeleteShouldNotEffectCountry_WhenCountryDoesNotExists()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act
            await _countryRepo.DeleteAsync(0000);

            // assert 
            // check if the databse state remains unchanged (no moives should be deleted)
            var existingCountries = _context.Countries.ToList();
            Assert.Equal(2, existingCountries.Count);

        }

        [Fact]
        public async Task AddShouldCreateNewMovie_WhenValidDataProvided()
        {

            // Arrange
            _countryRepo = new CountryRepo(_context);
            var country = new Country { Name = "Germany" };

            // Act
            var result = await _countryRepo.CreateAsync(country);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(country, result);// if the mvoie is equal to result or not? i.e if movie = "Inception" or not?

            // check if the movie is added 
            Assert.Equal(3, _context.Countries.Count());

        }

        [Fact]
        public async Task UpdateShouldModifyExistingMovie_WhenValidDataProvided()
        {
            // Arrange
            _countryRepo = new CountryRepo(_context);

            // Act
            var existingCountry = await _countryRepo.GetByIdAsync(1);

            if (existingCountry != null) // if award with the specified ID is found in the repo
            {
                // Modify the existing Award
                existingCountry.Name = "Updated Award";

                // Update the Award in the repository
                await _countryRepo.UpdateAsync(existingCountry);

                // Assert
                var updatedAward = await _countryRepo.GetByIdAsync(1);

                Assert.NotNull(updatedAward);
                Assert.Equal("Updated Award", updatedAward.Name);
            }
            else
            {
                Assert.True(false, "Award with the specified Id does not exist.");
            }

        }


    }
}
