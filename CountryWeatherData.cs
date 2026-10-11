public class CountryWeatherData
{
    private readonly Dictionary<string, WeatherData> data;

    public CountryWeatherData() : this([]) { }
    public CountryWeatherData(List<WeatherData> data)
    {
        this.data = [];
        foreach (var d in data)
        {
            Add(d);
        }
    }

    public void Add(WeatherData d)
    {
        if (data.ContainsKey(d.GetCity()))
        {
            throw new Exception($"Adding a duplicate city {d.GetCity()} for a coutry {d.GetCountry()}");
        }
        if (GetCountry() != null && d.GetCountry() != GetCountry())
        {
            throw new Exception($"Adding a different country city {d.GetCity()} from a contry {d.GetCountry()} for a country {GetCountry()}");
        }
        data[d.GetCity()] = d;
    }

    public string? GetCountry()
    {
        return GetCityCount() == 0 ? null : data.First().Value.GetCountry();
    }

    public int GetCityCount()
    {
        return data.Count();
    }

    public float GetMaxTemperature()
    {
        return data.Values.Max(d => d.GetTemperature());
    }

    public float GetMinTemperature()
    {
        return data.Values.Min(d => d.GetTemperature());
    }

    public float GetAvgTemperature()
    {
        return data.Values.Average(d => d.GetTemperature());
    }

    public override string ToString()
    {
        return string.Format(
            "{0} - {1} cities, avg: {2}, min: {3}, max: {4}",
            GetCountry(), GetCityCount(),
            CelciumValue.Convert(GetAvgTemperature()),
            CelciumValue.Convert(GetMinTemperature()),
            CelciumValue.Convert(GetMaxTemperature())
        );
    }
}