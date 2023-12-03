
using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Repositories.Interfaces
{
    public interface IDirectorRepo
    {
        // CRUD

        Task<IEnumerable<Director>>GetAllAsync();

        Task<Director> GetByIdAsync(int id);

        Task<Director>CreateAsync(Director director);

        Task<Director> UpdateAsync(Director director);

        Task<bool>DeleteAsync(int id);
    }
}
