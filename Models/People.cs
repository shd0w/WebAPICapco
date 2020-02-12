using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAPICapco.Models
{
    public class People
    {

        //Nome
        [JsonPropertyName("name")]
        public string Name { get; set; }

        //Altura
        [JsonPropertyName("height")]
        public string Height { get; set; }

        //Peso
        [JsonPropertyName("mass")]
        public string Mass { get; set; }
       
        [JsonPropertyName("hair_color")]
        public string HairColor { get; set; }

        [JsonPropertyName("skin_color")]
        public string SkinColor { get; set; }

        [JsonPropertyName("eye_color")]
        public string EyeColor { get; set; }

        [JsonPropertyName("birth_year")]
        public string BirthYear { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("homeworld")]
        public string HomeWorld { get; set; }

        //Lista com a URL dos filmes em que o personagem participou
        [JsonPropertyName("films")]
        public List<string> Films { get; set; }

        [JsonPropertyName("species")]
        public List<string> Species { get; set; }

        [JsonPropertyName("url")]
        public string PersonURL { get; set; }

    }
}