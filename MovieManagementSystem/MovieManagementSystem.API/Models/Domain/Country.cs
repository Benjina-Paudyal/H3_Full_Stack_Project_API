using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Models.Domain
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> MoviesProduced { get; set; } // one-to-many relation
    }
}
