using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Telegram.Bot.Types;
using WeSplit.Common.Entities.Org;
using WeSplit.SqlDatabase;
using DatabaseUser = WeSplit.Common.Entities.Org.User;

namespace WeSplit.Telegram
{
    public static class SqlQueries
    {
        public static async Task<Guid> CreateAuthenticationGarant(Update update)
        {
            using var dbContext = new SqlDbContext();

            var existingGarant = dbContext.AuthenticationGarants.FirstOrDefault(g => g.User.TelegramId == update.Message!.Chat.Id);

            if (existingGarant != null)
            {
                existingGarant.CreatedAt = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();
                return existingGarant.Id;
            }

            var guid = new Guid();

            var authIdentity = new Identity()
            {
                Guid = guid,
                Type = IdentityType.AuthenticationGarant,
            };

            var authenticaton = new AuthenticationGarant()
            {
                Id = guid,
                CreatedAt = DateTime.UtcNow,
                TimeToLive = TimeSpan.FromMinutes(5),
            };

            dbContext.Identities.Add(authIdentity);
            dbContext.AuthenticationGarants.Add(authenticaton);

            var user = dbContext.Users.FirstOrDefault(u => u.TelegramId == update.Message!.Chat.Id);

            if (user == null)
            {
                var userGuid = new Guid();

                var newUser = new DatabaseUser()
                {
                    Id = userGuid,
                    TelegramId = update.Message!.Chat.Id,
                    Name = update.Message!.Chat.FirstName ?? update.Message!.Chat.Id.ToString(),
                    CreateAt = DateTime.UtcNow,
                };

                var userIdentity = new Identity()
                {
                    Guid = guid,
                    Type = IdentityType.User,
                };

                dbContext.Identities.Add(userIdentity);
                dbContext.Users.Add(newUser);
            }
            else
            {
                // what to do if user is already auhenticated?
            }

            dbContext.SaveChanges();
            return guid;
        }
    }
}
