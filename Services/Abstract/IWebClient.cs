using System.Threading.Tasks;

namespace ytuyemekhane_telegram_bot.Services.Abstract
{
    public interface IWebClient
    {
        Task<string> GetAsync();
    }
}