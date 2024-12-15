using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    public partial class MainWindow : Window
    {
        private bool _isTimerRunning;
        private int _elapsedSeconds;
        int _elapsedMilliseconds;

        private bool EndGameStatus = false;
        private int _gameFieldHight = 5;
        private int _gameFieldWidth = 5;
        private float _minesPercent = 0.15f;

        private ModifyButton[,] _modifyButtons;
        private UserClick click = new UserClick();

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            SliderNumText = new TextBlock();
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isTimerRunning)
            {
                //MessageBox.Show("Секундомер уже запущен!");
                return;
            }

            _isTimerRunning = true;
            _elapsedSeconds = 0;

            var timerTask = RunTimer();
            var mainGameTask = RunGameProgram();

            await Task.WhenAll(timerTask, mainGameTask);

            _isTimerRunning = false;
            //MessageBox.Show("Игра завершена!");
        }

        private async Task RunTimer()
        {
            DateTime startTime = DateTime.Now;

            while (_isTimerRunning)
            {
                await Task.Delay(50);
                TimeSpan elapsedTime = DateTime.Now - startTime;

                Dispatcher.Invoke(() =>
                {
                    Timer.Text = $"{elapsedTime.Seconds}:{elapsedTime.Milliseconds:D3}";
                });
            }
            Timer.Text = "0:000";
        }


        private async Task RunGameProgram()
        {
            await Task.Run(() =>
            {
                Dispatcher.Invoke(() => Start());
            });
        }

        private void Start()
        {
            EndGameStatus = false;
            GamePanel.Visibility = Visibility.Visible;

            click.IsFirstClick = true;
            _modifyButtons = new ModifyButton[_gameFieldHight, _gameFieldWidth];

            for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    _modifyButtons[row, col] = new ModifyButton
                    {
                        FontSize = 16,
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray,
                        Row = row,
                        Col = col
                    };
                    _modifyButtons[row, col].Click += (s, args) => Button_Click(s, args);
                    _modifyButtons[row, col].MouseRightButtonDown += (s, args) => FlagButton_Click(s, args);
                }
            }
            DrawField();
            StartButton.Visibility = Visibility.Collapsed;
        }

        private void DrawField()
        {
            Grid gameField = new Grid();
            GamePanel.Children.Clear();
            GamePanel.Children.Add(gameField);
            gameField.Margin = new Thickness(50);

            for (int i = 0; i < _gameFieldHight; i++)
            {
                RowDefinition newRow = new RowDefinition();
                gameField.RowDefinitions.Add(newRow);
            }

            for (int i = 0; i < _gameFieldWidth; i++)
            {
                ColumnDefinition newColumn = new ColumnDefinition();
                gameField.ColumnDefinitions.Add(newColumn);
            }

            for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    if (_modifyButtons[row, col].Parent is Panel oldParent)
                    {
                        oldParent.Children.Remove(_modifyButtons[row, col]);
                    }
                    if (EndGameStatus)
                    {

                        _modifyButtons[row, col].IsEnabled = false;
                        if (_modifyButtons[row, col].IsMine)
                        {
                            Image flagImage = new Image
                            {
                                Source = new BitmapImage(new Uri("pack://application:,,,/Images/Mine.png")),
                                Stretch = Stretch.Uniform,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                HorizontalAlignment = HorizontalAlignment.Stretch
                            };

                            Viewbox viewbox = new Viewbox
                            {
                                Stretch = Stretch.Uniform,
                                Child = flagImage
                            };

                            Grid.SetRow(viewbox, row);
                            Grid.SetColumn(viewbox, col);
                            gameField.Children.Add(viewbox);
                            continue;
                        }
                        if (_modifyButtons[row, col].IsNumber) _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;
                    }
                    else
                    {
                        if (_modifyButtons[row, col].IsNumber && _modifyButtons[row, col].IsActive) _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;
                    }
                    Grid.SetRow(_modifyButtons[row, col], row);
                    Grid.SetColumn(_modifyButtons[row, col], col);
                    gameField.Children.Add(_modifyButtons[row, col]);

                }
            }
        }

        private void GenerateField()
        {
            int _minesNumber = (int)((float)(_gameFieldHight * _gameFieldWidth) * _minesPercent);
            int countMines = 0;

            /*for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    _modifyButtons[row, col].IsMine = false;
                    _modifyButtons[row, col].IsNumber = false;
                    _modifyButtons[row, col].IsNone = true;
                    _modifyButtons[row, col].IsActive = false;
                }
            }*/

            foreach (var butt in _modifyButtons)
            {
                _modifyButtons[butt.Row, butt.Col].IsMine = false;
                _modifyButtons[butt.Row, butt.Col].IsNumber = false;
                _modifyButtons[butt.Row, butt.Col].IsNone = true;
                _modifyButtons[butt.Row, butt.Col].IsActive = false;
            }

            while (countMines < _minesNumber)
            {
                int row = rand.Next(0, _gameFieldHight);
                int col = rand.Next(0, _gameFieldWidth);

                if (row >= 0 && row < _gameFieldHight &&
                    col >= 0 && col < _gameFieldWidth &&
                    !_modifyButtons[row, col].IsMine &&
                    !_modifyButtons[row, col].IsNumber &&
                    (row == 0 || !_modifyButtons[row - 1, col].IsMine) &&
                    (row == _gameFieldHight - 1 || !_modifyButtons[row + 1, col].IsMine) &&
                    (col == 0 || !_modifyButtons[row, col - 1].IsMine) &&
                    (col == _gameFieldWidth - 1 || !_modifyButtons[row, col + 1].IsMine))
                {
                    _modifyButtons[row, col].IsMine = true;
                    _modifyButtons[row, col].IsNone = false;
                    countMines++;
                }
            }

            /*for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    if (CountNearMines(row, col) > 0)
                    {
                        _modifyButtons[row, col].IsNumber = true;
                        _modifyButtons[row, col].IsNone = false;
                        _modifyButtons[row, col].NearMines = CountNearMines(row, col);
                    }
                }
            }*/
            foreach (var butt in _modifyButtons)
            {
                if (CountNearMines(butt.Row, butt.Col) > 0)
                {
                    _modifyButtons[butt.Row, butt.Col].IsNumber = true;
                    _modifyButtons[butt.Row, butt.Col].IsNone = false;
                    _modifyButtons[butt.Row, butt.Col].NearMines = CountNearMines(butt.Row, butt.Col);
                }
            }
        }

        private void EndGame()
        {
            _isTimerRunning = false;
            EndGameStatus = true;
            DrawField();
            StartButton.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            click.row = ((ModifyButton)sender).Row;
            click.col = ((ModifyButton)sender).Col;

            if (click.IsFirstClick)
            {
                GenerateField();
                while (_modifyButtons[click.row, click.col].IsMine || _modifyButtons[click.row, click.col].IsNumber) GenerateField();
                click.IsFirstClick = false;
                DrawField();
            }

            OpenCells(click.row, click.col);
            DrawField();

            if (_modifyButtons[click.row, click.col].IsMine)
            {
                EndGame();
                MessageBox.Show("Поздравляем, вы взорвались!");
            }
        }

        private bool CheckWinCondition()
        {
            foreach (var button in _modifyButtons)
            {
                if (button.IsMine && !button.IsFlagged) return false;
                if (!button.IsMine && button.IsFlagged) return false;
            }
            return true;
        }
        private void OpenCells(int row, int col)
        {
            if (row < 0 || row >= _gameFieldHight || col < 0 || col >= _gameFieldWidth)
                return;

            if (_modifyButtons[row, col].IsActive || _modifyButtons[row, col].IsMine)
                return;

            _modifyButtons[row, col].Hide();

            if (_modifyButtons[row, col].IsNumber)
                return;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    OpenCells(row + i, col + j);
                }
            }
        }

        private int CountNearMines(int row, int col)
        {
            int count = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < _gameFieldHight &&
                        newCol >= 0 && newCol < _gameFieldWidth &&
                        _modifyButtons[newRow, newCol].IsMine)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private void FlagButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (ModifyButton)sender;
            button.IsFlagged = !button.IsFlagged;

            if (button.IsFlagged)
            {
                Image flagImage = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Images/Flag.png")),
                    Stretch = Stretch.Uniform,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                Viewbox viewbox = new Viewbox
                {
                    Stretch = Stretch.Uniform,
                    Child = flagImage
                };

                button.Content = viewbox;
            }
            else
            {
                button.Content = null;
            }
            ///////////////////////////////////////////////////////////////
            if (CheckWinCondition() && !click.IsFirstClick)
            {
                EndGame();
                MessageBox.Show("Поздравляем, вы выиграли!");
            }
        }

        private void ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SliderNumText != null)
            {
                SliderNumText.Text = e.OldValue.ToString("F0");
            }
        }
    }
}


