namespace IoCThreatAnalyzer.Services;

public class WebFetchService
{
    private readonly HttpClient _client;

    public WebFetchService(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetHtmlAsync(string url)
    {
        try
        {
            return await _client.GetStringAsync(url);
        }
        catch
        {
            return string.Empty;
        }
    }
}