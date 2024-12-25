using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WeatherMenu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public enum DaysOfWeek
        {
            Monday = 1, 
            Tuesday = 2, 
            Wednesday = 3, 
            Thursday = 4,
            Friday = 5,
            Saturday = 6,
            Sunday = 7
        }
        //private Day[] Days = new Day[7];

        private void Start()
        {
            
        }
        private ObservableCollection<Day> _days;

        public ObservableCollection<Day> Days
        {
            get { return _days; }
            set { _days = value; }
        }

        public MainWindow()
        {
            InitializeComponent();

            Days = new ObservableCollection<Day>
            {
                new Day
                {
                    WeekDay = "Monday",
                    Date = DateTime.Now,
                    MaxTemp = 25,
                    MinTemp = 15,
                    Location = "City A",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1015,
                    WindSpeed = 5.5,
                    WindDirection = "North",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 20 }
                    }
                },
                new Day
                {
                    WeekDay = "Tuesday",
                    Date = DateTime.Now.AddDays(1),
                    MaxTemp = 23,
                    MinTemp = 14,
                    Location = "City B",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1012,
                    WindSpeed = 4.5,
                    WindDirection = "East",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 19 }
                    }
                },
                new Day
                {
                    WeekDay = "Wensday",
                    Date = DateTime.Now.AddDays(1),
                    MaxTemp = 23,
                    MinTemp = 14,
                    Location = "City C",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1012,
                    WindSpeed = 4.5,
                    WindDirection = "East",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 19 }
                    }
                },
                new Day
                {
                    WeekDay = "Thursday",
                    Date = DateTime.Now,
                    MaxTemp = 25,
                    MinTemp = 15,
                    Location = "City D",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1015,
                    WindSpeed = 5.5,
                    WindDirection = "North",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 20 }
                    }
                },
                new Day
                {
                    WeekDay = "Friday",
                    Date = DateTime.Now.AddDays(1),
                    MaxTemp = 23,
                    MinTemp = 14,
                    Location = "City E",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1012,
                    WindSpeed = 4.5,
                    WindDirection = "East",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 19 }
                    }
                },
                new Day
                {
                    WeekDay = "Saturday",
                    Date = DateTime.Now,
                    MaxTemp = 25,
                    MinTemp = 15,
                    Location = "City F",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1015,
                    WindSpeed = 5.5,
                    WindDirection = "North",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 20 }
                    }
                },
                new Day
                {
                    WeekDay = "Sunday",
                    Date = DateTime.Now,
                    MaxTemp = 25,
                    MinTemp = 15,
                    Location = "City G",
                    Wheather = WeatherCodes.Thunderstorm,
                    Pressure = 1015,
                    WindSpeed = 5.5,
                    WindDirection = "North",
                    DayInfo = new List<DayByHour>
                    {
                        new DayByHour { Time = DateTime.Now.AddHours(1), Temperature = 20 }
                    }
                }
            };

            DataContext = this;
        }

        private void DaysOfWeek1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailsButton.DataContext = DaysOfWeek1.SelectedItem;
        }
    }
}
