using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using ytuyemekhane_telegram_bot.Services.Abstract;

namespace ytuyemekhane_telegram_bot.Services
{
    public class BotService : IBotService
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IWebClient _webClient;
        private readonly IMemoryCache _cache;

        public BotService(ITelegramBotClient botClient, IWebClient webClient, IMemoryCache cache)
        {
            _botClient = botClient;
            _webClient = webClient;
            _cache = cache;
        }

        public async Task RecieveAsync(Update update)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageRecievedAsync(update.Message),
                _ => BotOnMessageRecievedAsync(update.Message)
            };

            await handler;
        }

        private async Task BotOnMessageRecievedAsync(Message message)
        {

            if (message.Type != MessageType.Text)
                return;

            var action= message.Text.Split(' ').First() switch
            {
                "/menu"=>BringMenu(_botClient,message),
                "/source"=>RedirecToSource(_botClient,message),
                "/help"=>Usage(_botClient,message),
                _=> Usage(_botClient,message)
            };
            var res= await action;

        }

        private async Task<Message> Usage(ITelegramBotClient bot, Message message)
        {
            const string usage = "Kullanım:\n" +
                                 "/menu   - Bugünün Menüsünü Getirir\n" +
                                 "/source - Telegram Botunun Kaynağını Gösterir\n" +
                                 "/help   - Mevcut Komutları Listeler\n";

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: usage,
                                                  replyMarkup: new ReplyKeyboardRemove());
        }

        private async Task<Message> BringMenu(ITelegramBotClient bot, Message message)
        {
            string result;
            var key = DateTime.Now.Date.ToShortDateString();
            if (!_cache.TryGetValue(key, out result))
            {
                result = await _webClient.GetAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                _cache.Set(key, result, cacheEntryOptions);

            }

            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: result,
                                                  replyMarkup: new ReplyKeyboardRemove());

        }

        private async Task<Message> RedirecToSource(ITelegramBotClient bot, Message message)
        {
            return await bot.SendTextMessageAsync(chatId: message.Chat.Id,
                                                  text: "https://github.com/bariscanyilmaz/ytuyemekhane-telegrambot",
                                                  replyMarkup: new ReplyKeyboardRemove());
        }
    }

}
