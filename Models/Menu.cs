using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ytuyemekhane_telegram_bot.Models
{
    public class Menu
    {
        [JsonPropertyName("date")]
        public Date Date { get; set; }

        [JsonPropertyName("lunch")]
        public List<string> Lunch { get; set; }

        [JsonPropertyName("dinner")]
        public List<string> Dinner { get; set; }

        public override string ToString()
        {
            return $"Tarih:{Date.ToString()}\nÖğlen Yemeği:11:30 - 14:30\n{string.Join("\n", Lunch)}\nAkşam Yemeği:17:30 - 20:30\n{string.Join("\n", Dinner)}";
        }
    }

}
