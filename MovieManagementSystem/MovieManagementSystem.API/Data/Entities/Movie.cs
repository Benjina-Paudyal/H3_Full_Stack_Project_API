using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieManagementSystem.API.Data.Domain
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Title { get; set; }

        public ICollection<Review> Reviews { get; set; } // One to many relation with reviews ; navigating properties

        public ICollection<MovieGenre> MoviesGenres { get; set; } // Many to many relation with genre through MovieGenre

        public ICollection<Award> Awards { get; set; } // one to many relation

        [ForeignKey("DirectorId")]
        public int DirectorId { get; set; } // foreign key 
        public Director Director { get; set; } // navigation property

        [ForeignKey("CountryId")]
        public int CountryId { get; set; } // foreign key 
        public Country Country { get; set; } // navigation property

        [ForeignKey("LanguageId")]
        public int LanguageId { get; set; } // foreign key 
        public Language Language { get; set; } // navigation property




        /* 

         You have defined this property as a navigaiton property in the respective domain models Director, Country, Language
         So, you need not to declare it here again. If you do, no problem but it will happen automaitcally by EFC. It like
        reaching the same destinaiton point through different paths. :)

         BUT for queries, you need to declare ...


         public int DirectorId { get; set; } // foreign key 
         public Director Director { get; set; } // navigation property


         public int CountryId { get; set; }
         public Country Country { get; set; } // 


         public int LanguageId { get; set; }
         public Language Language { get; set; } //   

         */






    }
}
