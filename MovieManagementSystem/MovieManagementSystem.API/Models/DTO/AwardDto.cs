namespace MovieManagementSystem.API.Models.DTO
{
    // Input DTO
    public class AwardInputDto
    {
        
        public string AwardName { get; set; }
        public int MovieId { get; set; }

    }

    // Output DTO

    public class AwardOutputDto
    {
        public int AwardId { get; set; }
        public string AwardName { get; set; }

        public int MovieId { get; set; }
        public MovieOutputDto Movie { get; set; }
    }
}
