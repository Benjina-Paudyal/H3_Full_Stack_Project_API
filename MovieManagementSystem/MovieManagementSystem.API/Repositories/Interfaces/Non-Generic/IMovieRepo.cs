using MovieManagementSystem.API.Models.Domain;

namespace MovieManagementSystem.API.Repositories.Interfaces
{
    public interface IMovieRepo
    {
        // CRUD 
        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie> GetByIdAsync(int id);

        Task<Movie> CreateAsync(Movie movie);

        Task<Movie> UpdateAsync(Movie movie);

        Task<bool> DeleteAsync(int id);
    }
}
