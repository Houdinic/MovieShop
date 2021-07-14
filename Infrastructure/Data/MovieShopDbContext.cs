using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        // DbSets as properties
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }

        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }

        // to use fluent API we need to override a nmethod OnModelCreating


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Movie>().HasMany(m => m.Genres).WithMany(g => g.Movies)
                .UsingEntity<Dictionary<string, object>>("MovieGenre",
                 m => m.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                 g => g.HasOne<Movie>().WithMany().HasForeignKey("MovieId"));
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> obj)
        {
            obj.ToTable("MovieCast");
            obj.HasKey(m => new { m.MovieId, m.CastId, m.Character });
            obj.Property(m => m.Character).HasMaxLength(450);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> obj)
        {
            obj.ToTable("Cast");
            obj.HasKey(c => c.Id);
            obj.Property(c => c.Name).HasMaxLength(128);
            obj.Property(c => c.ProfilePath).HasMaxLength(2084);

        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> obj)
        {
            obj.ToTable("MovieCrew");
            obj.HasKey(m => new { m.MovieId, m.CrewId, m.Department, m.Job });
            obj.Property(m => m.Department).HasMaxLength(128);
            obj.Property(m => m.Job).HasMaxLength(128);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> obj)
        {
            obj.ToTable("Review");
            obj.HasKey(r => new{ r.UserId,r.MovieId});
            //obj.HasKey(r => r.UserId);
            //obj.HasKey(r => r.MovieId);
            obj.Property(r => r.Rating).IsRequired();
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> obj)
        {
            obj.ToTable("Favorite");
            obj.HasKey(f => f.Id);
        }

        private void ConfigureUser(EntityTypeBuilder<User> obj)
        {
            obj.ToTable("User");
            obj.HasKey(u => u.Id);
            obj.Property(u => u.FirstName).HasMaxLength(128);
            obj.Property(u => u.LastLoginDateTime).HasMaxLength(128);
            obj.Property(u => u.Email).HasMaxLength(256);
            obj.Property(u => u.HashedPassword).HasMaxLength(1024);
            obj.Property(u => u.Salt).HasMaxLength(1024);
            obj.Property(u => u.PhoneNumber).HasMaxLength(16);
        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }
        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify all the Fluent API rules for this model
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Ignore(m => m.Rating);
        }
    }
}
