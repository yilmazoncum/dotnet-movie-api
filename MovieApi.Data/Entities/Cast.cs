namespace MovieApi.Data.Entities
{
    public class Cast
    {
        public Guid MovieId { get; set; }
        public Guid PersonId { get; set; }
        public string? Name { get; set; }
        public string? KnownForDepartment { get; set; }
        public string? Character { get; set; }
    }
}
