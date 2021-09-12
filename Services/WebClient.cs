using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using ytuyemekhane_telegram_bot.Services.Abstract;
using ytuyemekhane_telegram_bot.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ytuyemekhane_telegram_bot.Options;

namespace ytuyemekhane_telegram_bot.Services
{
    public class WebClient : IWebClient
    {

        private readonly HttpClient _client;
        private readonly ApiConfig _config;
        public WebClient(HttpClient client,IOptions<ApiConfig> config)
        {
            _client = client;
            _config=config.Value;
            
        }

        public async Task<string> GetAsync()
        {
            var response = await _client.GetAsync(_config.Uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var menus = JsonSerializer.Deserialize<List<Menu>>(content);
                return menus[0].ToString();
            }

            return "Bu tarihe ait herhangi bir menu yok";

        }
    }
}