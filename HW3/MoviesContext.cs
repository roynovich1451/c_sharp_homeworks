using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCreation
{
    public class MoviesContext : DbContext
    {
        private const string connectionString = "server=(localdb)\\MSSQLLocalDb;database=c:\\databases\\DBManageMoviesCore_2;trusted_connection=true";

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
           /*many to many relation - movie to Actor*/
            modelBuilder.Entity<ActorMovie>()
                .HasKey(sc => new { sc.ActorId, sc.MovieSerial });
            modelBuilder.Entity<ActorMovie>()
                .HasOne(sc => sc.Actor)
                .WithMany(s => s.ActorMovies)
                .HasForeignKey(sc => sc.ActorId);
            modelBuilder.Entity<ActorMovie>()
                 .HasOne(sc => sc.Movie)
                 .WithMany(m => m.ActorMovies)
                 .HasForeignKey(sc => sc.MovieSerial);

            /*one to many relation- movie to director*/
            modelBuilder.Entity<Director>()
                .HasMany(d => d.Movies)
                .WithOne(m => m.Director);

            /*one to one/zero relation-oscar to movie*/
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Oscar)
                .WithOne(o => o.BestMotionPicture)
                .HasForeignKey<Oscar>(o => o.MovieSerial)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Oscar> Oscars { get; set; }
        public DbSet<Actor> Actors { get; set; }


    }
}
