
using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Repositories.Interfaces
{
    public interface ILanguageRepo
    {

        Task<IEnumerable<Language>> GetAllAsync();

        Task<Language> GetByIdAsync(int id);

        Task<Language> CreateAsync(Language language);

        Task<Language> UpdateAsync(Language language);

        Task<bool> DeleteAsync(int id);

    }
}
