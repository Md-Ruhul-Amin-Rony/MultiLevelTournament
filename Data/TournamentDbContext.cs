using Microsoft.EntityFrameworkCore;
using MultiLevelTournament.Models;

namespace MultiLevelTournament.Data
{
    public class TournamentDbContext:DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options) : base(options)
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentPlayer> TournamentPlayers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>()
                .HasOne(t => t.ParentTournament)
                .WithMany(t => t.SubTournaments)
                .HasForeignKey(t => t.ParentTournamentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TournamentPlayer>()
                .HasKey(pt => new { pt.PlayerId, pt.TournamentId });

            modelBuilder.Entity<TournamentPlayer>()
                .HasOne(pt => pt.Player)
                .WithMany(p => p.PlayerTournaments)
                .HasForeignKey(pt => pt.PlayerId);

            modelBuilder.Entity<TournamentPlayer>()
                .HasOne(pt => pt.Tournament)
                .WithMany(t => t.PlayerTournaments)
                .HasForeignKey(pt => pt.TournamentId);
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Player>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        //        entity.Property(e => e.Age).IsRequired();
        //    });


        //    modelBuilder.Entity<Tournament>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);



        //        entity.HasOne(e => e.ParentTournament)
        //              .WithMany(e => e.SubTournaments)
        //              .HasForeignKey(e => e.ParentTournamentId)
        //              .OnDelete(DeleteBehavior.Restrict);
        //    });


        //    modelBuilder.Entity<TournamentPlayer>(entity =>
        //    {
        //        entity.HasKey(e => new { e.TournamentId, e.PlayerId });

        //        entity.HasOne(e => e.Tournament)
        //              .WithMany(e => e.TournamentPlayers)
        //              .HasForeignKey(e => e.TournamentId)
        //              .OnDelete(DeleteBehavior.Cascade);

        //        entity.HasOne(e => e.Player)
        //              .WithMany(e => e.TournamentPlayers)
        //              .HasForeignKey(e => e.PlayerId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
