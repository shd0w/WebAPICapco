using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPICapco.Models
{
    public class Root
    {
        [JsonPropertyName("films")]
        public string Films { get; set; }

        [JsonPropertyName("people")]
        public string People { get; set; }

        [JsonPropertyName("planets")]
        public string Planets { get; set; }

        [JsonPropertyName("species")]
        public string Species { get; set; }

        [JsonPropertyName("starships")]
        public string Starships { get; set; }

        [JsonPropertyName("vehicles")]
        public string Vehicles { get; set; }
    }
}