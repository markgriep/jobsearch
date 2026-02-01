using Refit;
using System.Text.Json.Serialization;

namespace jobsearch.Interfaces
{
    public interface IOpenAIClient
    {
        [Post("/v1/responses")]
        Task<OpenAIResponse> GetResponse(
            [Header("Authorization")] string authorization,
            [Body] OpenAIRequest request);
    }

    public class OpenAIRequest
    {
        public required string Model { get; set; }
        public required string Input { get; set; }
    }

    public class OpenAIResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("output")]
        public List<OutputMessage> Output { get; set; } = new();

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; } = new();
    }

    public class OutputMessage
    {
        [JsonPropertyName("content")]
        public List<ContentBlock> Content { get; set; } = new();
    }

    public class ContentBlock
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;
    }

    public class Usage
    {
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }

        [JsonPropertyName("input_tokens")]
        public int InputTokens { get; set; }

        [JsonPropertyName("output_tokens")]
        public int OutputTokens { get; set; }
    }
}
