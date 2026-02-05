namespace jobsearch.Configuration;

public class OpenAiSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public double Temperature { get; set; } = 0.8d;
}
