using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.API.Models.Domain
{
    public class Review
    {
        [Key]

        public int ReviewId { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }


        [ForeignKey("MovieId")]
        public int MovieId { get; set; } // foreign key
        public Movie Movie { get; set; } // navigation property

       
    }
}
