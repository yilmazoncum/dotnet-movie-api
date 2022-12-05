namespace MovieApi.Data.Entities
{
    public partial class Filmography
    {
        public int MovieId { get; set; }

        public int? PersonId { get; set; }

        public string? Title { get; set; }

        public string? Character { get; set; }
    
    }
}
