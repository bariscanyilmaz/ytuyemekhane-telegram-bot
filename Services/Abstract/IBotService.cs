using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ytuyemekhane_telegram_bot.Services.Abstract
{
    public interface IBotService
    {
        Task RecieveAsync(Update update);
    }

}