using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;

namespace MovieManagementSystem.API.Repositories.Implementation.NonGeneric
{
    public class CountryRepo : ICountryRepo
    {
        public readonly MovieDbContext _context;
        public CountryRepo(MovieDbContext context)
        {
            _context = context;

        }

        // crud operations


        // get all
        public async Task<IEnumerable<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }


        // GET BY ID

        public async Task<Country> GetByIdAsync(int id)
        {
            return await _context.Countries            // retrieves country by id
              .FirstOrDefaultAsync(x => x.CountryId == id);

        }


        //CREATE

        public async Task<Country> CreateAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return country;
        }


        //UPDATE

        public async Task<Country> UpdateAsync(Country country)
        {
            _context.Entry(country).State = EntityState.Modified; // EFC automatic update
            await _context.SaveChangesAsync();
            return country;
        }


        // DELETE

        public async Task<bool> DeleteAsync(int id)
        {
            var country = _context.Countries.FirstOrDefault(x => x.CountryId == id);
            if (country != null)
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return true;

            }

            return false;
        }

    }
}
