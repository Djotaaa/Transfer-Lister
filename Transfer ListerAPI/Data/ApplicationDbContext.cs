using Microsoft.EntityFrameworkCore;
using Transfer_ListerAPI.Models;

namespace Transfer_ListerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    Id = "GK",
                    PositionName = "Goalkeeper",
                },
                new Position
                {
                    Id = "CB",
                    PositionName = "Centre Back",
                },
                new Position
                {
                    Id = "LB",
                    PositionName = "Left Back",
                },
                new Position
                {
                    Id = "RB",
                    PositionName = "Right Back",
                },
                new Position
                {
                    Id = "DM",
                    PositionName = "Defensive Midfielder",
                },
                new Position
                {
                    Id = "CM",
                    PositionName = "Central Midfielder",
                },
                new Position
                {
                    Id = "LM",
                    PositionName = "Left Midfielder",
                },
                new Position
                {
                    Id = "RM",
                    PositionName = "Right Midfielder",
                },
                new Position
                {
                    Id = "LW",
                    PositionName = "Left Wing",
                },
                new Position
                {
                    Id = "RW",
                    PositionName = "Right Wing",
                },
                new Position
                {
                    Id = "AMC",
                    PositionName = "Attacking Midfielder",
                },
                new Position
                {
                    Id = "ST",
                    PositionName = "Striker",
                }
                );
        }
    }
}
