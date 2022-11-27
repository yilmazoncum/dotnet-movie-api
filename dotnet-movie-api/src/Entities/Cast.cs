using System;
using System.Collections.Generic;

namespace dotnet_movie_api.src.Models;

public partial class Cast
{
    public int? MovieId { get; set; }

    public int? PersonId { get; set; }

    public string? Name { get; set; }

    public string? KnownForDepartment { get; set; }

    public string? Character { get; set; }
}
