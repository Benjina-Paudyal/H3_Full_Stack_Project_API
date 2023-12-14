using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Data.Domain
{
    public class Director
    {
        [Key]
        public int DirectorId { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> MovieDirectors { get; set; } // one-to-many relation 
    }
}
