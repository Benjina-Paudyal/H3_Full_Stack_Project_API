using MovieManagementSystem.API.Data.Domain;

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


        Task<List<Award>> GetAwardsByNameAsync(string awardName);

   





    }
}
