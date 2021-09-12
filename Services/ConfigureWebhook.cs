using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using ytuyemekhane_telegram_bot.Options;

namespace ytuyemekhane_telegram_bot.Services
{
    public class ConfigureWebhook : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly BotConfig _botConfig;

        public ConfigureWebhook(IServiceProvider serviceProvider,IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration=configuration;
            _botConfig=_configuration.GetSection("BotConfig").Get<BotConfig>();

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            var webHookAdress = $"{_botConfig.HostAddress}/bot/{_botConfig.BotToken}";
            await botClient.SetWebhookAsync(webHookAdress,default, default, default, Array.Empty<UpdateType>(), default, cancellationToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

            await botClient.DeleteWebhookAsync(default, cancellationToken);
        }
    }

}