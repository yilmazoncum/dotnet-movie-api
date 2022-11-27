using System;
using System.Collections.Generic;

namespace dotnet_movie_api.src.Models;

public partial class Person
{
    public DateTime? Birthday { get; set; }

    public DateTime? Deathday { get; set; }

    public bool? Gender { get; set; }

    public int Id { get; set; }

    public string? ImdbId { get; set; }

    public string? KnownForDepartment { get; set; }

    public string? Name { get; set; }

    public string? PlaceOfBirth { get; set; }
}
