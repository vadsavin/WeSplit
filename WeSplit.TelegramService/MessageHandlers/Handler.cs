using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using WeSplit.Common.Entities.Org;
using WeSplit.SqlDatabase;

namespace WeSplit.Telegram.MessageHandlers
{
    public class Handler
    {
        private TelegramBotClient _client;
        private ILogger<TelegramService> _log;

        public Handler(TelegramBotClient client, ILogger<TelegramService> log)
        {
            _client = client;
            _log = log;
        }

        public Task HandleUpdate(Update update)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message?.Text is { })
            {
                return HandleTextMessage(update);
            }
                
            return Task.CompletedTask;
        }

        public async Task HandleTextMessage(Update update)
        {
            var message = update.Message;
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            _log.LogInformation($"Received a Text '{messageText}' message in chat {chatId}.");

            if (messageText.StartsWith("/"))
            {

            }

            if (Guid.TryParse(messageText, out var parsedGuid))
            {
                using var dbContext = new SqlDbContext();

                var identity = dbContext.Identities.FirstOrDefault(e => e.Guid == parsedGuid);

                if (identity == null)
                {
                    await _client.SendTextMessageAsync(chatId, "Unknown identity, add new");
                    dbContext.Identities.Add(new Identity()
                    {
                        Guid = parsedGuid,
                        Type = IdentityType.None
                    });
                    await dbContext.SaveChangesAsync();
                    return;
                }

                await _client.SendTextMessageAsync(chatId, identity.Type.ToString());
            }
        }

        public Task HandleCommand(Update update, string text)
        {
            text = text.Replace("/","");

            return text switch
            {
                "authenticate" => Task.CompletedTask,
                _ => Task.CompletedTask
            };
        }
    }
}
