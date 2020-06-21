using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManageMovies
{
    public partial class dbContext : DbContext
    {
        public dbContext()
        {
        }

        public dbContext(DbContextOptions<dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActorMovie> ActorMovie { get; set; }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Director> Directors { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Oscar> Oscars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDb;database=c:\\databases\\DBManageMovies;trusted_connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovie>(entity =>
            {
                entity.HasKey(e => new { e.ActorId, e.MovieSerial });

                entity.HasIndex(e => e.MovieSerial);

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.ActorMovies)
                    .HasForeignKey(d => d.ActorId);

                entity.HasOne(d => d.MovieSerialNavigation)
                    .WithMany(p => p.ActorMovies)
                    .HasForeignKey(d => d.MovieSerial);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieSerial);

                entity.HasIndex(e => e.DirectorId);

                entity.Property(e => e.ImdbScore).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.DirectorId);
            });

            modelBuilder.Entity<Oscar>(entity =>
            {
                entity.HasKey(e => e.Year);

                entity.HasIndex(e => e.BestActorId);

                entity.HasIndex(e => e.BestActressId);

                entity.HasIndex(e => e.BestDirectorId);

                entity.HasIndex(e => e.MovieSerial)
                    .IsUnique()
                    .HasFilter("([MovieSerial] IS NOT NULL)");

                entity.Property(e => e.Year).ValueGeneratedNever();

                entity.HasOne(d => d.BestActor)
                    .WithMany(p => p.OscarsBestActors)
                    .HasForeignKey(d => d.BestActorId);

                entity.HasOne(d => d.BestActress)
                    .WithMany(p => p.OscarsBestActresses)
                    .HasForeignKey(d => d.BestActressId);

                entity.HasOne(d => d.BestDirector)
                    .WithMany(p => p.Oscars)
                    .HasForeignKey(d => d.BestDirectorId);

                entity.HasOne(d => d.MovieSerialNavigation)
                    .WithOne(p => p.Oscars)
                    .HasForeignKey<Oscar>(d => d.MovieSerial);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
