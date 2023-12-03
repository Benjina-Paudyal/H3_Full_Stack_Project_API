using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Models.Domain
{
    public class Director
    {
        [Key]
        public int DirectorId { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> MovieDirectors { get; set; } // one-to-many relation 
    }
}
