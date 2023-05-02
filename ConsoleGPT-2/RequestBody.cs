using System;
using Newtonsoft.Json;
namespace ConsoleGPT_2
{
    public class RequestBody
    {
        [JsonProperty("model")]
        public string Model { get; }

        [JsonProperty("prompt")]
        public string Prompt { get; }

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; }

        [JsonProperty("temperature")]
        public float Temperature { get; }

        public RequestBody (string prompt)
        {
            Model = "text-davinci-003";
            Prompt = prompt;
            MaxTokens = 500;
            Temperature = 1.0f;
        }
    }
}