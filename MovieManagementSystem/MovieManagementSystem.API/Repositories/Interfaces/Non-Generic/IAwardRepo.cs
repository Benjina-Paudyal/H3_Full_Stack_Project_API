using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Repositories.Interfaces
{
    public interface IAwardRepo
    {
        // CRUD 
        Task<IEnumerable<Award>> GetAllAsync();

        Task<Award> GetByIdAsync(int id);

        Task<Award> CreateAsync(Award award);

        Task<Award> UpdateAsync(Award award);

        Task<bool> DeleteAsync(int id);
        


    }
}
