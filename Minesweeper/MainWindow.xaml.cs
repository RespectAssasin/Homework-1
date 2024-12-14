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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool EndGameStatus = false;
        private int _gameFieldHight = 10;
        private int _gameFieldWidth = 10;
        private float _minesPercent = 0.15f;

        private ModifyButton[,] _modifyButtons;
        private UserClick click = new UserClick();

        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
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
                        if (_modifyButtons[row, col].IsMine) _modifyButtons[row, col].Background = Brushes.Red;
                        if (_modifyButtons[row, col].IsNumber) _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;
                        _modifyButtons[row, col].Click -= (s, args) => Button_Click(s, args);                                       //доделать
                        _modifyButtons[row, col].MouseRightButtonDown -= (s, args) => FlagButton_Click(s, args);
                        //_modifyButtons[row, col].IsEnabled = false;
                    } else
                    {
                        if (_modifyButtons[row, col].IsNumber && _modifyButtons[row, col].IsActive) _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;
                    }

                    //if (_modifyButtons[row, col].IsMine) _modifyButtons[row, col].Background = Brushes.Red;

                    //Console.WriteLine($"Row: {row}, Col: {col}, IsNumber: {_modifyButtons[row, col].IsNumber}, IsActive: {_modifyButtons[row, col].IsActive}");

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

            for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    _modifyButtons[row, col].IsMine = false;
                    _modifyButtons[row, col].IsNumber = false;
                    _modifyButtons[row, col].IsNone = true;
                    _modifyButtons[row, col].IsActive = false;
                }
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

            for (int row = 0; row < _gameFieldHight; row++)
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
            }
        }

        private void EndGame()
        {
            EndGameStatus = true;
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

            /*if (sender is Button button)
            {
                var scaleTransform = new ScaleTransform(1.0, 1.0);
                button.RenderTransform = scaleTransform;
                button.RenderTransformOrigin = new Point(0.5, 0.5);

                var animation = new DoubleAnimation(1.0, 0.9, TimeSpan.FromMilliseconds(100));

                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            }*/

            OpenCells(click.row, click.col);
            DrawField();//боль
            /*if (sender is Button button)
            {
                var shadowEffect = button.Effect as DropShadowEffect ?? new DropShadowEffect
                {
                    Color = Colors.Black,
                    Direction = 315,
                    ShadowDepth = 5,
                    BlurRadius = 10,
                    Opacity = 0.5
                };

                button.Effect = shadowEffect;

                var depthAnimation = new DoubleAnimation
                {
                    From = 5,
                    To = 10,
                    Duration = TimeSpan.FromMilliseconds(100),
                    AutoReverse = true
                };

                var opacityAnimation = new DoubleAnimation
                {
                    From = 0.5,
                    To = 0.8,
                    Duration = TimeSpan.FromMilliseconds(100),
                    AutoReverse = true
                };

                shadowEffect.BeginAnimation(DropShadowEffect.ShadowDepthProperty, depthAnimation);
                shadowEffect.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation);
            }*/

            if (_modifyButtons[click.row, click.col].IsMine)
            {
                EndGame();
                DrawField();
                MessageBox.Show("Поздраляю, вы взорвались!!!\nСиквел игры \"Собери себя по частям\" находится в разработке, не ждите");
            }
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

            if (_modifyButtons[row, col].IsMine) return 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newRow = row + i;
                    int newCol = col + j;

                    if (newRow >= 0 && newRow < _gameFieldHight &&
                        newCol >= 0 && newCol < _gameFieldWidth &&
                        !(i == 0 && j == 0))
                    {
                        if (_modifyButtons[newRow, newCol].IsMine)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void FlagButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
