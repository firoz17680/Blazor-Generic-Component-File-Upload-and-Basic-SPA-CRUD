using System;
using System.Collections.Generic;
using System.Text;

namespace Day3Training24032021.Shared
{
    public class WeatherForecast
    {
        public int Sno { get; set; }
        
        public bool Checked { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
