using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Data.Domain
{
    public class MovieActor // junction table in a many to many relation between 'Movie' and 'Actor'
    {
        [Key]
        public int Id { get; set; }

        public int MovieId { get; set; } // foreign key
        public Movie Movie { get; set; } // navigation property


        public Actor Actor { get; set; }
        public int ActorId { get; set; }
    }
}
