using Microsoft.EntityFrameworkCore;
using SAmuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    //DbContext provides logic for EF Core to interact wity you database.
    public class SamuraiContext :DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Qoutes{ get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
              "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=SamuraiAppDataFirstLook")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name},Microsoft.Extensions.Logging.LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(s => s.Battles)
                .WithMany(b => b.Samurais)
                .UsingEntity<BattleSamurai>
                (bs => bs.HasOne<Battle>().WithMany(), bs => bs.HasOne<Samurai>().WithMany())
                .Property(bs => bs.DateJoined)
                .HasDefaultValueSql("getdate()");
        }
    }
}
