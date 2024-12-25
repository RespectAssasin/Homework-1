using System;
using System.Collections.Generic;
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
        public enum WindDirection
        {
            West = 1,
            East = 2,
            Nord = 3,
            Sud = 4
        }

        public enum WeatherCodes
        {
            ClearSky = 0,
            Windy = 1,
            Overcast = 2,
            Fog = 3,
            SlightRain = 4,
            HeavyRain = 5,
            Snowfall = 6,
            Thunderstorm = 7
        }
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
        private Day[] Days = new Day[7];
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 1; i == 7; i++)
            {
                DaysOfWeek day = (DaysOfWeek)i;
                Days[i].WeekDay = day.ToString();
                Days[i].
            }

            Start();
        }

        private void Start()
        {
            
        }

    }
}
