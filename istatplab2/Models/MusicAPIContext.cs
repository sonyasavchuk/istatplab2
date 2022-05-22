using Microsoft.EntityFrameworkCore;

namespace istatplab2.Models
{
    public class MusicAPIContext : DbContext
    {
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Playlists> Playlists { get; set; }
        public virtual DbSet<PlaylistsTracks> PlaylistsTracks { get; set; }
        public virtual DbSet<Tracks> Tracks { get; set; }

        public MusicAPIContext(DbContextOptions<MusicAPIContext> options ): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Albums>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.Artists)
                .WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });

        modelBuilder.Entity<Artists>(entity =>
        {

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            
        });

        modelBuilder.Entity<Playlists>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            

            entity.HasMany(c => c.Tracks)
                .WithMany(a => a.Playlists)
                .UsingEntity<PlaylistsTracks>(
                    configureRight => configureRight
                        .HasOne(d => d.Tracks)
                        .WithMany()
                        .HasForeignKey(d => d.TrackId)
                        .OnDelete(DeleteBehavior.ClientCascade),
                    configureLeft => configureLeft
                        .HasOne(d => d.Playlists)
                        .WithMany()
                        .HasForeignKey(d => d.PlaylistId)
                        .OnDelete(DeleteBehavior.ClientCascade),
                        builder => builder
                        .ToTable("PlaylistsTracks")
                        
                );
        });
        

        modelBuilder.Entity<Tracks>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.miliseconds)
                .IsRequired();

            entity.HasOne(d => d.Albums)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .OnDelete(DeleteBehavior.ClientCascade);
            entity.HasOne(d => d.Genres)
                .WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientCascade);

            
        });
        modelBuilder.Entity<Genres>(entity =>
        {

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);
            
        });
        
    }

    }
}
