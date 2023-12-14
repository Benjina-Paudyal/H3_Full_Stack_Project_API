using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.API.Data.Domain
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }


        [ForeignKey("UserId")]
        public int UserId { get; set; }// foreign key
        public User User { get; set; } // navigation property


        [ForeignKey("MovieId")]
        public int MovieId { get; set; } // foreign key
        public Movie Movie { get; set; } // navigation property


    }
}
