using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Repositories.Interfaces
{
    public interface ICountryRepo
    {
        // CRUD

        Task<IEnumerable<Country>> GetAllAsync();

        Task<Country> GetByIdAsync(int id);

        Task<Country> CreateAsync(Country country);

        Task<Country> UpdateAsync(Country country);

        Task<bool>DeleteAsync(int id);


    }
}
