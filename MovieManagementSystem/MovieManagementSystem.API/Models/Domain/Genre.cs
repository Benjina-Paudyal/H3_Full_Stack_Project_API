using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MovieManagementSystem.API.Models.Domain
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        public string Name { get; set; }

        public ICollection<MovieGenre> Movies { get; set; } // one-to-many relation between 'Genre' and 'Movie'
    }
}
