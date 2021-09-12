using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using ytuyemekhane_telegram_bot.Options;
using ytuyemekhane_telegram_bot.Services;
using ytuyemekhane_telegram_bot.Services.Abstract;

namespace ytuyemekhane_telegram_bot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            BotConfig = configuration.GetSection("BotConfig").Get<BotConfig>();
        }

        public IConfiguration Configuration { get; }
        private BotConfig BotConfig { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApiConfig>(Configuration.GetSection("ApiConfig"));

            services.AddHostedService<ConfigureWebhook>();

            services.AddHttpClient("tgwebhook")
                   .AddTypedClient<ITelegramBotClient>(httpClient
                       => new TelegramBotClient(BotConfig.BotToken, httpClient));

            services.AddScoped<IBotService, BotService>();
            services.AddHttpClient<IWebClient, WebClient>();
            services.AddMemoryCache();
            services.AddControllers().AddNewtonsoftJson();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {

                var token = BotConfig.BotToken;
                endpoints.MapControllerRoute(name: "tgwebhook",
                                             pattern: $"bot/{token}/",
                                             new { controller = "Home", action = "Post" }
                                             );
                endpoints.MapControllers();

            });
        }
    }
}
