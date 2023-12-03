using System.ComponentModel.DataAnnotations;

namespace MovieManagementSystem.API.Models.Domain
{
    public class Language
    {
        [Key]
        public int LanguageId { get; set; }

        public string Name { get; set; }

        public ICollection<Movie> Movies { get; set; } // one-to-many
    }
}
