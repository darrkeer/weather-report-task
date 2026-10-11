# weather-report-task

## Запуск
```
dotnet run
```
## Пример запуска
```
weather-report-task main ? ❯ dotnet run
Cities:
Moscow, Russia +11°C
Khabarovsk, Russia +6°C
Saint-Petersburg, Russia +9°C
Vienna, Austria +14°C
Izhevsk, Russia +7°C
Perm, Russia +9°C
NhaTrang, Vietnam +25°C
Villach, Austria +4°C
=====================================
Countries:
Russia - 5 cities, avg: +8.4°C, min: +6°C, max: +11°C
Austria - 2 cities, avg: +9°C, min: +4°C, max: +14°C
Vietnam - 1 cities, avg: +25°C, min: +25°C, max: +25°C
```
## Комментарии по коду

Основной класс `Programm.cs`

- создан класс `WebCrawler`, который упрощает доступ к сети
- был создан класс `WeatherData`, который по городу загружает остальную информацию с нужного сайта и красиво их выводит
- был создан класс `CountryWeatherData`, который агрегирует все данные по одной стране и красиво их выводит

## Возможные улучшения
- добавление автотестов (но без внешней библиотеки не очень хочется) 