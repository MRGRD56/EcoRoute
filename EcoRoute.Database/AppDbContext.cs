using EcoRoute.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoRoute.Database
{
    public sealed class AppDbContext : DbContext
    {
        private static bool _isMigrated = false;

        public AppDbContext()
        {
            if (_isMigrated) return;
            
            Database.Migrate();
            _isMigrated = true;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Filename=data.db");
        }
        
        // public DbSet<Sensor> Sensors { get; set; }
        // public DbSet<SensorData> SensorDataList { get; set; }
    }
}