using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Repositories.Implementation.NonGeneric
{
    public class DirectorRepo : IDirectorRepo
    {
        private readonly MovieDbContext _context;
        public DirectorRepo(MovieDbContext context)
        {
            _context = context;
        }

        // CRUD OPERATIONS


        // GET ALL
        public async Task<IEnumerable<Director>> GetAllAsync()
        {
            return await _context.Directors.ToListAsync();
        }


        // GET BY ID
        public async Task<Director> GetByIdAsync(int id)
        {
            return await _context.Directors
                .FirstOrDefaultAsync(x => x.DirectorId == id); // retreives director by i (filtering the elements)
        }


        // CREATE
        public async Task<Director> CreateAsync(Director director)
        {
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return director;
        }


        // UPDATE
        public async Task<Director> UpdateAsync(Director director)
        {
            _context.Entry(director).State = EntityState.Modified; // EFC automatic update
            await _context.SaveChangesAsync();
            return director;
        }


        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var director = _context.Directors.FirstOrDefault(x => x.DirectorId == id);
            {
                if (director != null)
                {
                    _context.Directors.Remove(director);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }






    }
}
