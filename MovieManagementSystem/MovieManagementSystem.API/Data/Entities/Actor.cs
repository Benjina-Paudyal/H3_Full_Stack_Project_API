using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Data.Domain
{
    public class Actor
    {
        [Key]
        public int ActorId { get; set; }

        public string Name { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } // one to many relation between 'Actor' and 'MovieActor' i.e one actor can be
                                                                 // associated with multiple instances of 'MovieActor' where each 'MovieActor'
                                                                 // instance represents the actor's participation in  different movies.
    }
}
