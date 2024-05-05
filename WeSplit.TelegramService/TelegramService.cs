using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WeSplit.Common.Entities.Org;
using WeSplit.Common.Secrets;
using WeSplit.SqlDatabase;
using TelegramUser = Telegram.Bot.Types.User;

namespace WeSplit.Telegram
{
    public class TelegramService : BackgroundService
    {
        private TelegramBotClient _client;
        private ILogger<TelegramService> _log;
        private TelegramUser _me;

        public TelegramService(ISecretResolver secretResolver, ILogger<TelegramService> log)
        {
            _client = new TelegramBotClient(secretResolver.TelegramApiKey);
            _log = log;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            ReceiverOptions receiverOptions = new()
            {
                Limit = null,
                AllowedUpdates = [ UpdateType.Message ] // receive all update types except ChatMember related updates
            };

            _client.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: stoppingToken
            );

            _me = await _client.GetMeAsync();

            _log.LogInformation($"Start listening for @{_me.Username}");

            while (!stoppingToken.IsCancellationRequested) ;
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            _log.LogInformation($"Received a '{messageText}' message in chat {chatId}.");

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
            else if (messageText.StartsWith("/"))
            {

            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            _log.LogError(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
