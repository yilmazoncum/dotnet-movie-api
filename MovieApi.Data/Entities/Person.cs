using Microsoft.AspNetCore.Mvc;

namespace MovieApi.Data.Entities
{
    [BindProperties]
    public class Person
    {
        public Guid Id { get; set; }
        public int? ApiId { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string? ImdbId { get; set; }
        public string? KnownForDepartment { get; set; }
        public string? Name { get; set; }
        public string? PlaceOfBirth { get; set; }
    }
}
