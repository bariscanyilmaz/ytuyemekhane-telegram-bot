using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using ytuyemekhane_telegram_bot.Services.Abstract;

namespace ytuyemekhane_telegram_bot.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost("/bot/{token}")]
        public async Task<IActionResult> Post([FromServices] IBotService botService, [FromBody] Update update)
        {
            await botService.RecieveAsync(update);
            return Ok();
        }
    }
}
