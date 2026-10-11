using System.Threading.Tasks.Dataflow;

const string CITIES_DOWNLOAD_PAGE = "https://gistpad.com/raw/vk-task-14";
const string delimeter = "=====================================";


List<string> GetCities(string downloadedPage)
{
    using var reader = new StringReader(downloadedPage);
    string? line;
    List<string> result = new() { };
    while ((line = reader.ReadLine()) != null)
    {
        result.Add(line);
    }
    return result;
}

void WriteOutput(IEnumerable<WeatherData> cityData, IEnumerable<CountryWeatherData> countryData)
{
    Console.WriteLine("Cities:");
    foreach (var d in cityData)
    {
        Console.WriteLine(d);
    }

    Console.WriteLine(delimeter);

    Console.WriteLine("Countries:");
    foreach (var c in countryData)
    {
        Console.WriteLine(c);
    }
}



// main programm
WebCrawler crawler = new WebCrawler();
var cities = GetCities(await crawler.DownloadPage(CITIES_DOWNLOAD_PAGE));

List<WeatherData> data = new();
foreach (var city in cities)
{
    var newData = new WeatherData(city);
    await newData.DownloadData(crawler);
    data.Add(newData);
}

Dictionary<string, CountryWeatherData> countries = new();
foreach (var d in data)
{
    // making sure that this city is present
    countries.TryAdd(d.GetCountry(), new CountryWeatherData());

    countries[d.GetCountry()].Add(d);
}

WriteOutput(data, countries.Values);
