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
            return $"Tarih:{Date.ToString()}\nÖğlen Yemeği:\n{string.Join("\n", Lunch)}\nAkşam Yemeği:\n{string.Join("\n", Dinner)}";
        }
    }

}