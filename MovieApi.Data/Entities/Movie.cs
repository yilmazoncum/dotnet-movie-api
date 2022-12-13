namespace MovieApi.Data.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public int? ApiId { get; set; }
        public string? Budget { get; set; }
        public string? ImdbId { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Overview { get; set; }
        public string? PosterPath { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? Revenue { get; set; }
        public int? Runtime { get; set; }
        public string? Title { get; set; }
        public decimal? VoteAverage { get; set; }
        public int? VoteCount { get; set; }
    }
}
