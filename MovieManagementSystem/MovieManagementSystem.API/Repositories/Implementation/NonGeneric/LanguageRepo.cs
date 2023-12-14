using Microsoft.EntityFrameworkCore;
using MovieManagementSystem.API.Data;
using MovieManagementSystem.API.Data.Domain;
using MovieManagementSystem.API.Repositories.Interfaces;
using System.IO;

namespace MovieManagementSystem.API.Repositories.Implementation.NonGeneric
{
    public class LanguageRepo : ILanguageRepo
    {
        private readonly MovieDbContext _context;
        public LanguageRepo(MovieDbContext context)
        {
            _context = context;
        }

        // CRUD Operations


        // GET ALL
        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }


        // GET BY ID
        public async Task<Language> GetByIdAsync(int id)
        {
            return await _context.Languages
                 .FirstOrDefaultAsync(x => x.LanguageId == id); // retreives director by i (filtering the elements)
        }



        // CREATE
        public async Task<Language> CreateAsync(Language language)
        {
            _context.Languages.Add(language);
            await _context.SaveChangesAsync();
            return language;
        }

        // UPDATE
        public async Task<Language> UpdateAsync(Language language)
        {
            _context.Entry(language).State = EntityState.Modified; // EFC automatic update
            await _context.SaveChangesAsync();
            return language;
        }

        // DELETE
        public async Task<bool> DeleteAsync(int id)
        {
            var language = _context.Languages.FirstOrDefault(x => x.LanguageId == id);
            {
                if (language != null)
                {
                    _context.Languages.Remove(language);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }

        }

    }
}

