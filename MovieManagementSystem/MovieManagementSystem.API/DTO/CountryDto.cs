namespace MovieManagementSystem.API.DTO
{
    public class CountryInputDto
    {
        public string Name { get; set; }
    }

    public class CountryOutputDto
    {
        public int CountryId { get; set; }

        public string Name { get; set; }
    }
}
