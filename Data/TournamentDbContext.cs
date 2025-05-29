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


        
    }
}
