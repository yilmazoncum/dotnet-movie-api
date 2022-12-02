using System;
using System.Collections.Generic;

namespace dotnet_movie_api.src.Models;

public partial class MoviesPerson
{
    public int MovieId { get; set; }

    public string? OriginalTitle { get; set; }

    public string? Overview { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Title { get; set; }

    public string? Character { get; set; }
}
