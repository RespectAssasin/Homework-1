using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _userName;
        private int maxRows = 4;
        private int maxCols = 4;
        private TimeSpan _timeRemaining;
        private Random _random;

        private Button _clickButton = null;
        private int _clickCount;

        private List<(string Name, int Score)> _leaderboard = new List<(string Name, int Score)>();

        public MainWindow()
        {
            InitializeComponent();
            _random = new Random();
        }
        private void GoToGame(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            _userName = UserNameBox.Text;
            GamePanel.Visibility = Visibility.Visible;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Collapsed;
            TimerTextBlock.Visibility = Visibility.Visible;
            GameArea.Visibility = Visibility.Visible;
            GameGrid.Visibility = Visibility.Visible;

            _timeRemaining = TimeSpan.FromMinutes(0.5);
            TimerTextBlock.Text = _timeRemaining.ToString(@"mm\:ss");

            _clickCount = 0;
            UpdateClickCounter();
          
            await Task.Delay(100);
            /*MessageBox.Show($"GameGrid: Width={GameGrid.ActualWidth}, Height={GameGrid.ActualHeight}");*/
            CreateGameButton();

            await StartTimer();
            EndGame();
        }
        private void UpdateClickCounter()
        {
            ClickCounterTextBlock.Text = $"Счёт: {_clickCount}";
        }
        
        private async Task StartTimer()
        {
            while (_timeRemaining > TimeSpan.Zero)
            {
                TimerTextBlock.Text = _timeRemaining.ToString(@"mm\:ss");
                await Task.Delay(1000);
                _timeRemaining -= TimeSpan.FromSeconds(1);
            } 
        }

        private void EndGame()
        {
            MessageBox.Show($"Время вышло! Ваш счёт: {_clickCount}", "Игра завершена");

            /*_playerNames.Add(_userName);
            _playerScores.Add(_clickCount);*/

            _leaderboard.Add((_userName, _clickCount));
            UpdateLeaderboard();

            GameGrid.Children.Clear();
            GameArea.Visibility = Visibility.Collapsed;
            TimerTextBlock.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Visible;

            ShowLeaderboard();
        }
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            LeaderboardPanel.Visibility = Visibility.Collapsed;
            MenuPanel.Visibility = Visibility.Visible;
        }

        private void CreateGameButton()
        {
            if (GameGrid.ActualWidth <= 0 || GameGrid.ActualHeight <= 0)
            {
                MessageBox.Show("GameGrid не готов. Проверьте размеры.");
                return;
            }

            if (_clickButton != null)
            {
                GameGrid.Children.Remove(_clickButton);
            }

            _clickButton = new Button
            {
                Content = "Жми!!!",
                Width = 100,
                Height = 50,
                FontSize=15
            };

            _clickButton.Click += ClickButton_Click;

            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < maxRows; i++)
                GameGrid.RowDefinitions.Add(new RowDefinition());

            for (int i = 0; i < maxCols; i++)
                GameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            int randomRow = _random.Next(maxRows);
            int randomCol = _random.Next(maxCols);

            Grid.SetRow(_clickButton, randomRow);
            Grid.SetColumn(_clickButton, randomCol);

            GameGrid.Children.Add(_clickButton);
        }


        private void ClickButton_Click(object sender, RoutedEventArgs e)
        {
            _clickCount++;
            UpdateClickCounter();
            CreateGameButton();
        }

        private void UpdateLeaderboard()
        {
          //_leaderboard.Add((_userName, _clickCount));
            _leaderboard = _leaderboard.OrderByDescending(player => player.Score).Take(5).ToList();
        }
        private void ShowLeaderboard()
        {
            LeaderboardListBox.Items.Clear();
            foreach (var player in _leaderboard)
            {
                LeaderboardListBox.Items.Add($"{player.Name}: {player.Score}");
            }

            GamePanel.Visibility = Visibility.Collapsed;
            LeaderboardPanel.Visibility = Visibility.Visible;
        }
    }
}
