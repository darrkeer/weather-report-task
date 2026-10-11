public class WebCrawler : IDisposable
{
    private readonly HttpClient client;

    public WebCrawler()
    {
        client = new HttpClient();
    }

    public void Dispose() => client.Dispose();

    public async Task<string> DownloadPage(string url)
    {
        // wont be checking http errors, just hand them to main, either way they will be in console
        return await client.GetStringAsync(url);
    }

    public async Task<string> DownloadCity(string cityName)
    {
        string url = $"https://wttr.in/{cityName}?format=j1";
        return await DownloadPage(url);
    }
}