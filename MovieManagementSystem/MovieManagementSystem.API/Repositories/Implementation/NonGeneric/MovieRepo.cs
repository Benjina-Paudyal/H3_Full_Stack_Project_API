using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Repositories.Implementation.NonGeneric
{
    public class MovieRepo : IMovieRepo
    {
        public readonly MovieDbContext _context;

        public MovieRepo(MovieDbContext context)
        {
            _context = context
;
        }


        // GET All
        async Task<IEnumerable<Movie>> IMovieRepo.GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }


        // GET BY ID
        public async Task<Movie> GetByIdAsync(int id)
        {

            return await _context.Movies
                .Include(m => m.Director)
                .FirstOrDefaultAsync(c => c.MovieId == id);
        }


        // CREATE
        public async Task<Movie> CreateAsync(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie;

        }



        // UPDATE
        public async Task<Movie> UpdateAsync(Movie movie)
        {
            _context.Entry(movie).State = EntityState.Modified;  // EFC automatic update
            await _context.SaveChangesAsync();
            return movie;
        }




        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var movie = _context.Movies.FirstOrDefault(x => x.MovieId == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }


    }
}
