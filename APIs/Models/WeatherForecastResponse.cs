using System;

namespace APIs.Models
{
    public class WeatherForecastResponse
    {
        public DateTime Date { get; set; } 
        
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }

    }
   
}
