using Telegram.Bot.Types;
using WeSplit.Common.Entities.Org;
using WeSplit.SqlDatabase;
using DatabaseUser = WeSplit.Common.Entities.Org.User;

namespace WeSplit.Telegram
{
    public static class SqlQueries
    {
        public static Task<Guid> CreateAuthenticateRequest(Update update)
        {
            using var dbContext = new SqlDbContext();

            var guid = new Guid();

            var authIdentity = new Identity()
            {
                Guid = guid,
                Type = IdentityType.AuthenticationToken,
            };

            var authenticaton = new Authentication()
            {
                Id = guid,
                CreatedAt = DateTime.UtcNow,
                TimeToLive = TimeSpan.FromMinutes(5),
            };

            dbContext.Identities.Add(authIdentity);
            dbContext.Authentications.Add(authenticaton);

            var user = dbContext.Users.FirstOrDefault(u => u.TelegramId == update.Message!.Chat.Id);

            if (user == null)
            {
                var userGuid = new Guid();

                var newUser = new DatabaseUser()
                {
                    Id = userGuid,
                    TelegramId = update.Message!.Chat.Id,
                    Name = update.Message!.Chat.FirstName,
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
                throw new NotImplementedException("Implement user reauth");
            }

            dbContext.SaveChanges();
            return Task.FromResult(guid);
        }
    }
}
