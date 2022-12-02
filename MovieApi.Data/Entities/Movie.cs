using System;
using System.Collections.Generic;

namespace dotnet_movie_api.src.Models;

public partial class Movie
{
    public int Id { get; set; }

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
