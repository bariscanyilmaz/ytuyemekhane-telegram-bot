using System.Text.Json.Serialization;

namespace ytuyemekhane_telegram_bot.Models
{
    public class Date
    {
        [JsonPropertyName("day")]
        public int Day { get; set; }
        
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Day}/{Month}/{Year}";
        }
    }

}