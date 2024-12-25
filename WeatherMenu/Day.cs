using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMenu
{
    public class Day
    {
        public string WeekDay { get; set; }
        public DateTime Date { get; set; }
        public int MaxTemp { get; set; }
        public int MinTemp { get; set; }

        public string Location { get; set; }
        public string Wheather { get; set; }
        public double Pressure { get; set; }
        public double WindSpeed {  get; set; }
        public string WindDirection { get; set; }
        public List<DayByHour> DayInfo { get; set; }
    }
}
