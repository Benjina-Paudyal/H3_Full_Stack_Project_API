using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Implementation;
using MovieManagementSystem.API.Repositories.Implementation.NonGeneric;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieManagementSystem.Test.RepositoriesTest.GnericRepoTest
{
    public class GenericRepoTest
    {
        private DbContextOptions<MovieDbContext> _options;
        private MovieDbContext _context;
        private GenericRepo<T> _movieRepo;

        public GenericRepoTest()
        {
            _options = new DbContextOptionsBuilder<MovieDbContext>()
               .UseInMemoryDatabase("TestDatabase").Options;

            _context = new MovieDbContext(_options);
            _movieRepo = new GenericRepo<T>(_context);
            _context.Database.EnsureDeleted();

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

        [Fact]
        public void GetAllShoulReturnListOfMovies_WhenMovieExists()
        {
            // Arrange

            _movieRepo = new GenericRepo<T>(_context);

            // Act
            var result = _movieRepo.GetAll();

            // Assert 
            Assert.NotNull(result);
            Assert.IsType<List<Movie>>(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetAllShouldReturnEmptyList_WhenMovieDoesNotExists()
        {
            // Arrange

            _context.Movies.RemoveRange(_context.Movies);
            _context.SaveChangesAsync();
            _movieRepo = new GenericRepo<T>(_context);

            // Act
            var result = _movieRepo.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Movie>>(result);
            Assert.Empty(result);

        }

    }
}
