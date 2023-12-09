using MovieManagementSystem.API.Models.Domain;
using System.Collections;

namespace MovieManagementSystem.API.Models.DTO
{

    public class MovieInputDto
    {

        public string Title { get; set; }

        public int DirectorId { get; set; }

        public int  CountryId { get; set; }

        public int  LanguageId { get; set; }

    }


    public class MovieOutputDto
    {

        public int MovieId { get; set; }

        public string Title { get; set; }

       // public DirectorOutputDto Director { get; set; }

      //  public CountryOutputDto Country { get; set; }

      //  public LanguageOutputDto Language { get; set;}



    }

}   
