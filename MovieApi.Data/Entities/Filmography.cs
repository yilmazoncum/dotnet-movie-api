namespace MovieApi.Data.Entities
{
    public class Filmography
    {
        public Guid MovieId { get; set; }
        public Guid PersonId { get; set; }
        public string Title { get; set; }
        public string Character { get; set; }
        
        public Guid id { get; set; }
    }
}
