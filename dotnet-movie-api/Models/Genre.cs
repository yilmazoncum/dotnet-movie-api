using System;
using System.Collections.Generic;

namespace dotnet_movie_api.models;

public partial class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
