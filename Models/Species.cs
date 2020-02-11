using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;

namespace WebAPICapco.Models
{
    public class Species
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string SpeciesURL { get; set; }
    }
}