using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.API.Data.Domain
{
    public class Award
    {
        [Key]
        public int AwardId { get; set; }

        public string AwardName { get; set; }


        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; } // navigation property : it allows to navigate from an 'Award' to the associated 'Movie'
                                         // If an award is associated with a movie, we can access details of that movie through this property
                                         // many to one relation from 'Award' to 'Movie'


    }
}
