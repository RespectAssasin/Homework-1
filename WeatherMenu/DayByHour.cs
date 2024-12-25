using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMenu
{
    public class DayByHour
    {
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float RelativeHumidity { get; set; }
        public float SurfasePressure { get; set; }
        public float WindSpeed { get; set; }
        public int WindDirection { get; set; }
        public string Weather { get; set; }
    }
}
