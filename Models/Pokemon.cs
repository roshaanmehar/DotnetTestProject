using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TestApp.Models
{
    public class Pokemon
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }

        [JsonPropertyName("sprites")]
        public PokemonSprites Sprites { get; set; } = new();

        [JsonPropertyName("types")]
        public List<PokemonType> Types { get; set; } = new();

        [JsonPropertyName("abilities")]
        public List<PokemonAbility> Abilities { get; set; } = new();
    }

    public class PokemonSprites
    {
        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; } = string.Empty;
    }

    public class PokemonType
    {
        [JsonPropertyName("type")]
        public TypeInfo Type { get; set; } = new();
    }

    public class TypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }

    public class PokemonAbility
    {
        [JsonPropertyName("ability")]
        public AbilityInfo Ability { get; set; } = new();
    }

    public class AbilityInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}