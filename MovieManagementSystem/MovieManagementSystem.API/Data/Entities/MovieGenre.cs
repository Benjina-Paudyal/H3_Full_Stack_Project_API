using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.API.Data.Domain
{
    public class MovieGenre // junction table in  many to many relation between 'Movie' and 'Genre'
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MovieId")]
        public int MovieId { get; set; } // foreign key
        public Movie Movie { get; set; } // navigation property



        [ForeignKey("GenreId")]
        public int GenreId { get; set; } // foreign key
        public Genre Genre { get; set; } // navigation proeprty
    }
}
