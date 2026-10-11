using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

public class WeatherData
{
    private float Temperature;
    private string CountryName;
    private string CityName;

    private bool Broken;

    public WeatherData(float temperature, string countryName, string cityName)
    {
        Temperature = temperature;
        CountryName = countryName;
        CityName = cityName;
        Broken = false;
    }

    public WeatherData(string cityName) : this(0, "", cityName)
    {
        Broken = true;
    }

    public async Task DownloadData(WebCrawler crawler)
    {
        var page = await crawler.DownloadCity(CityName);
        var root = JsonNode.Parse(page);

        if (root == null)
        {
            throw new JsonException($"Bad json structure of a page for city: {CityName}");
        }
        Parse(root);
    }

    private void CheckBroken()
    {
        if (Broken)
        {
            throw new Exception("Weather data is not created yet");
        }
    }

    private void Parse(JsonNode root)
    {
        var country = (string?)root["nearest_area"]?[0]?["country"]?[0]?["value"];
        if (country == null)
        {
            throw new JsonException($"Could not find a country of a city: {CityName}");
        }
        CountryName = country;

        var temperature = (string?)root["current_condition"]?[0]?["temp_C"];
        if (temperature == null)
        {
            throw new JsonException($"Could not find a temperature of a city: {CityName}");
        }
        try
        {
            Temperature = float.Parse(temperature);
        }
        catch (Exception)
        {
            throw new JsonException($"Format of a temperature of a city {CityName} is wrong: {temperature}");
        }

        Broken = false;
    }

    public override string ToString()
    {
        return $"{GetCity()}, {GetCountry()} {CelciumValue.Convert(GetTemperature())}";
    }

    public float GetTemperature()
    {
        CheckBroken();
        return Temperature;
    }

    public string GetCountry()
    {
        CheckBroken();
        return CountryName;
    }

    public string GetCity()
    {
        return CityName;
    }
}