using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Work_25._11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Player> LeaderBoard = new List<Player>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private string UserName;
        private Random _random = new Random();

        private DateTime _startTime;
        private TimeSpan diff;
        private bool _isRunning = false;

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            UserName = UserNameBox.Text;
            UserInputPanel.Visibility = Visibility.Collapsed;
            StartTestPanel.Visibility = Visibility.Visible;
            UserNameBox.Text = string.Empty;
        }

         private async void StartTest_Click(object sender, RoutedEventArgs e)
        {
            StartTestPanel.Visibility = Visibility.Collapsed;
            //TimerBox.Text = string.Empty;
            ReactionTestPanel.Visibility = Visibility.Visible;

            StopButton.IsEnabled = false;
            await Task.Delay(_random.Next(3000, 7000));
            StopButton.IsEnabled = true;

            _startTime = DateTime.Now;
            _isRunning = true;

            while(_isRunning)
            {
                await Task.Delay(1);
                diff = DateTime.Now - _startTime;
                TimerBox.Text = diff.ToString(@"mm\:ss\.fff");
            }

            MessageBox.Show($"Ваше время: {diff.ToString(@"mm\:ss\.fff")}", "Результат");

            //StartTestPanel.Visibility= Visibility.Collapsed;

            Player player = new Player(UserName, diff);
            LeaderBoard.Add(player);

            UpdateLeaderboardDisplay();

            //

            //LeaderboardPanel.Visibility= Visibility.Visible;
            
        }
        
        private void StopTest_Click(object sender, RoutedEventArgs e)
        {
            _isRunning = false;
            StopButton.IsEnabled = false;
        }

        private void UpdateLeaderboardDisplay()
        {
            var sortedPlayers = LeaderBoard.OrderBy(p => p.Time).ToList();

            Player1Text.Text = string.Empty;
            Player2Text.Text = string.Empty;
            Player3Text.Text = string.Empty;
            Player4Text.Text = string.Empty;
            Player5Text.Text = string.Empty;

            if (sortedPlayers.Count > 0) Player1Text.Text = $"1. {sortedPlayers[0].Username}: {sortedPlayers[0].Time}";
            if (sortedPlayers.Count > 1) Player2Text.Text = $"2. {sortedPlayers[1].Username}: {sortedPlayers[1].Time}";
            if (sortedPlayers.Count > 2) Player3Text.Text = $"3. {sortedPlayers[2].Username}: {sortedPlayers[2].Time}";
            if (sortedPlayers.Count > 3) Player4Text.Text = $"4. {sortedPlayers[3].Username}: {sortedPlayers[3].Time}";
            if (sortedPlayers.Count > 4) Player5Text.Text = $"5. {sortedPlayers[4].Username}: {sortedPlayers[4].Time}";
        }

        private void PlayerMenuButt(object sender, RoutedEventArgs e)
        {
            UserInputPanel.Visibility = Visibility.Visible;

            StartTestPanel.Visibility = Visibility.Collapsed;
            ReactionTestPanel.Visibility = Visibility.Collapsed;
            LeaderboardPanel.Visibility = Visibility.Collapsed;

        }

        private void TestMenuButt(object sender, RoutedEventArgs e)
        {
            StartTestPanel.Visibility = Visibility.Visible;

            UserInputPanel.Visibility = Visibility.Collapsed;
            ReactionTestPanel.Visibility = Visibility.Collapsed;
            LeaderboardPanel.Visibility = Visibility.Collapsed;
        }

        private void LeaderBoardMenuButt(object sender, RoutedEventArgs e)
        {
            LeaderboardPanel.Visibility = Visibility.Visible;

            UserInputPanel.Visibility = Visibility.Collapsed;
            StartTestPanel.Visibility = Visibility.Collapsed;
            ReactionTestPanel.Visibility = Visibility.Collapsed;
        }
    }
}
