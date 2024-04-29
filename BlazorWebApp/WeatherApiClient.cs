using static BlazorWebApp.Components.Pages.Weather;

namespace BlazorWebApp
{
    public class WeatherApiClient(HttpClient httpClient)
    {
        public async Task<WeatherForecast[]> GetWeatherForecastAsync()
        {
            var requestUrl = Environment.GetEnvironmentVariable("Services__weatherapi");
            return await httpClient.GetFromJsonAsync<WeatherForecast[]>(requestUrl) ?? [];
        }
    }
}
