using Microsoft.EntityFrameworkCore;
using WeSplit.Common.Entities.Consumable;
using WeSplit.Common.Entities.Org;
using WeSplit.Common.Settings;

namespace WeSplit.SqlDatabase
{
    public class SqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Recipient> Recipients { get; set; }

        public DbSet<RecipientItem> RecipientItems { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Identity> Identities { get; set; }

        public DbSet<AuthenticationGarant> AuthenticationGarants { get; set; }

        public SqlDbContext() : base()
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
             options.UseSqlite(SettingsProvider.Settings.SqLiteConnectionString);
        }
    }
}
