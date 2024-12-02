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
using System.Windows.Threading;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void GoToGame(object sender, RoutedEventArgs e)
        {
            MenuPanel.Visibility = Visibility.Collapsed;
            GamePanel.Visibility = Visibility.Visible;
        }

        private DispatcherTimer _gameTimer;
        private TimeSpan _timeRemaining;
        private Random _random;
        private Button _clickButton;
        private int _clickCount;

        public MainWindow()
        {
            InitializeComponent();
            _random = new Random();
            _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _gameTimer.Tick += GameTimer_Tick;
        }
        private async void StartTimer()
        {
            while (_timeRemaining > TimeSpan.Zero)
            {
                TimerTextBlock.Text = _timeRemaining.ToString(@"mm\:ss");
                await Task.Delay(1000);
                _timeRemaining -= TimeSpan.FromSeconds(1);
            }

            EndGame();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.Visibility = Visibility.Collapsed;
            TimerTextBlock.Visibility = Visibility.Visible;
            GameArea.Visibility = Visibility.Visible;

            _timeRemaining = TimeSpan.FromMinutes(1);
            TimerTextBlock.Text = _timeRemaining.ToString(@"mm\:ss");

            _clickCount = 0;
            UpdateClickCounter();

            StartTimer();
            CreateGameButton();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            _timeRemaining -= TimeSpan.FromSeconds(1);
            TimerTextBlock.Text = _timeRemaining.ToString(@"mm\:ss");
            if (_timeRemaining <= TimeSpan.Zero)
            {
                _gameTimer.Stop();
                EndGame();
            }
        }

        private void EndGame()
        {
            MessageBox.Show($"Время вышло! Ваш счёт: {_clickCount}", "Игра завершена");
            GameGrid.Children.Clear();
            GameArea.Visibility = Visibility.Collapsed;
            TimerTextBlock.Visibility = Visibility.Collapsed;
            StartButton.Visibility = Visibility.Visible;
        }

        private void CreateGameButton()
        {
            if (_clickButton != null)
            {
                GameGrid.Children.Remove(_clickButton);
            }

            _clickButton = new Button
            {
                Content = "Жми!!!",
                Width = 80,
                Height = 40
            };
            _clickButton.Click += ClickButton_Click;

            int maxRows = 10;
            int maxCols = 10;

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

        private void UpdateClickCounter()
        {
            ClickCounterTextBlock.Text = $"Счёт: {_clickCount}";
        }
    }
}
