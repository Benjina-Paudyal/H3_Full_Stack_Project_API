using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Models.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Repositories.Implementation.NonGeneric
{
    public class AwardRepo : IAwardRepo
    {
        public readonly MovieDbContext _context;

        public AwardRepo(MovieDbContext context) // DI
        {
            _context = context;
        }



        // GET ALL

        public async Task<IEnumerable<Award>> GetAllAsync()
        {
            return await _context.Awards.Include(a=> a.Movie).ToListAsync();
        }


        // Get By Id
        public async Task<Award> GetByIdAsync(int id)
        {
            return await _context.Awards            // retrieves an award by id
                 .Include(a => a.Movie)             // including the related Movie
                  .FirstOrDefaultAsync(x => x.AwardId == id);
        }


        // CREATE
        public async Task<Award> CreateAsync(Award award)
        {
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();
            return award;

        }



        // UPDATE
        public async Task<Award> UpdateAsync(Award award)
        {
            _context.Entry(award).State = EntityState.Modified; // EFC automatic update 
            await _context.SaveChangesAsync();
            return award;

        }



        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var award = _context.Awards.FirstOrDefault(x => x.AwardId == id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }



    }
}
