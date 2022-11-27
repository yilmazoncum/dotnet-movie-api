﻿using System;
using System.Collections.Generic;
using dotnet_movie_api.models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_movie_api.DataAccess;

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

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MoviesPerson> MoviesPeople { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=movieDb;Trusted_Connection=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cast>(entity =>
        {
            entity.HasNoKey();

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

        modelBuilder.Entity<MoviesPerson>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies_P__42EB372EEF6EAB1E");

            entity.ToTable("Movies_Person");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movieID");
            entity.Property(e => e.Character)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("character");
            entity.Property(e => e.OriginalTitle)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("original_title");
            entity.Property(e => e.Overview)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("overview");
            entity.Property(e => e.ReleaseDate)
                .HasColumnType("date")
                .HasColumnName("release_date");
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
            entity.Property(e => e.Birthday)
                .HasColumnType("date")
                .HasColumnName("birthday");
            entity.Property(e => e.Deathday)
                .HasColumnType("date")
                .HasColumnName("deathday");
            entity.Property(e => e.Gender).HasColumnName("gender");
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
