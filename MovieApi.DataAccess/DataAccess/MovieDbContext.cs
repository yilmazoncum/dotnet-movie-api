using Microsoft.EntityFrameworkCore;
using MovieApi.Data.Entities;

namespace dotnet_movie_api.src.DataAccess;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
            
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cast> Casts { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Filmography> Filmographies { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cast>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Casts__42EB372EDAA0394D");

            entity.Property(e => e.Character)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("character");
            entity.Property(e => e.KnownForDepartment)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("known_for_department");
            entity.Property(e => e.MovieId).HasColumnName("movieID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PersonId).HasColumnName("personID");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movies__3213E83F7CA3A895");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApiId)
                .ValueGeneratedNever()
                .HasColumnName("api_id");
            entity.Property(e => e.Budget)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("budget");
            entity.Property(e => e.ImdbId)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("imdb_id");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("original_title");
            entity.Property(e => e.Overview)
                .HasMaxLength(216)
                .IsUnicode(false)
                .HasColumnName("overview");
            entity.Property(e => e.PosterPath)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("poster_path");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("date")
                .HasColumnName("release_date");
            entity.Property(e => e.Revenue).HasColumnName("revenue");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.Title)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.VoteAverage)
                .HasColumnType("numeric(5, 3)")
                .HasColumnName("vote_average");
            entity.Property(e => e.VoteCount).HasColumnName("vote_count");
        });

        modelBuilder.Entity<Filmography>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Filmogra__42EB372E34A92F1F");

            entity.ToTable("Filmographies");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movieID");
            entity.Property(e => e.PersonId)
                .ValueGeneratedNever()
                .HasColumnName("PersonID");
            entity.Property(e => e.Character)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("character");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Persons__3213E83F1CEB8AB5");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApiId)
                .ValueGeneratedNever()
                .HasColumnName("api_id");
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Deathday)
                .HasColumnType("date")
                .HasColumnName("deathday");
            entity.Property(e => e.ImdbId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imdb_id");
            entity.Property(e => e.KnownForDepartment)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("known_for_department");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("place_of_birth");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
