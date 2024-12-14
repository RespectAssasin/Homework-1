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

                    if (_modifyButtons[row, col].IsMine) _modifyButtons[row, col].Background = Brushes.Red;
                    if (_modifyButtons[row, col].IsNumber) _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;

                    /*if (_modifyButtons[row, col].IsMine)
                    {
                        _modifyButtons[row, col].Background = Brushes.Red;
                    }
                    if (_modifyButtons[row, col].IsNumber)
                    {
                        _modifyButtons[row, col].Content = _modifyButtons[row, col].NearMines;
                    }*/

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
            for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    _modifyButtons[row,col].IsEnabled = false;
                    StartButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            click.row = ((ModifyButton)sender).Row;
            click.col = ((ModifyButton)sender).Col;

            if (click.IsFirstClick)
            {
                /*if (sender is ModifyButton button)
                {
                    click.row = button.Row;
                    click.col = button.Col;
                }*/

                if (click.row >= 0 && click.row < _gameFieldHight &&
                    click.col >= 0 && click.col < _gameFieldWidth)
                {
                    GenerateField();
                    while (_modifyButtons[click.row, click.col].IsMine || _modifyButtons[click.row, click.col].IsNumber)
                    {
                        GenerateField();
                    }
                    click.IsFirstClick = false;
                    DrawField();
                    //MessageBox.Show($"первое {click.row},{click.col}");
                }
                else
                {
                    MessageBox.Show($" {click.row}, {click.col}Некорректные координаты первого клика!");
                }
                

            }
            //MessageBox.Show($"{click.row},{click.col}");
            if (sender is Button button)
            {
                var scaleTransform = new ScaleTransform(1.0, 1.0);
                button.RenderTransform = scaleTransform;
                button.RenderTransformOrigin = new Point(0.5, 0.5);

                var animation = new DoubleAnimation(1.0, 0.9, TimeSpan.FromMilliseconds(100));

                scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
                scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
            }
            OpenCells(click.row, click.col);
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
            ((ModifyButton)sender).IsEnabled = false;

            if (_modifyButtons[click.row,click.col].IsMine)
            {
                EndGame();
                MessageBox.Show("\nТы проиграл!!!");
            }
        }

        private void OpenCells(int Row, int Col)
        {
            for (int col = Col-1; col < Col+1;col++)
            {
                if (col == Col || col == _gameFieldWidth || col == 0) continue;
                if (_modifyButtons[Row, col].IsNone) // обработать края
                {
                    OpenCells(Row, col);
                    _modifyButtons[Row,col].IsEnabled = false;
                }
            }
            for (int row = Row - 1; row < Row + 1; row++)
            {
                if (row == Row || row == _gameFieldHight || row == 0) continue;
                if (_modifyButtons[row, Col].IsNone)
                {
                    OpenCells(row, Col);
                    _modifyButtons[row, Col].IsEnabled = false;
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

        /*private void GenerateGameField()
        {
            //ClearArr(_modifyButtons);
            //_modifyButtons = new bool[_gameFieldWidth, _gameFieldHight];
            _minesNumber = (int)((float)(_gameFieldHight * _gameFieldWidth) * _minesPercent);

            int placedMines = 0;

            while (placedMines < _minesNumber)
            {
                int row = rand.Next(0, _gameFieldHight);
                int col = rand.Next(0, _gameFieldWidth);

                if (!_modifyButtons[col, row] &&
                    (col == 0 || !_modifyButtons[col - 1, row]) &&
                    (col == _gameFieldWidth - 1 || !_modifyButtons[col + 1, row]) &&
                    (row == 0 || !_modifyButtons[col, row - 1]) &&
                    (row == _gameFieldHight - 1 || !_modifyButtons[col, row + 1]))
                {
                    _modifyButtons[col,row] = true;
                    placedMines++;
                }
                else
                {
                    placedMines--;
                }
            }
        }*/


        /*private void GenerateUIField()
        {
            _buttons = new CoolButt[_gameFieldHight, _gameFieldWidth];
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
                    CoolButt cellButton = new CoolButt
                    {
                        FontSize = 16,
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray
                    };

                    cellButton.Click += (s, e) => OnCellButtonClick(row, col);

                    if (_modifyButtons[row, col])
                    {
                        cellButton.Content = "M";
                        cellButton.Background = Brushes.Red;
                        //cellButton.Name = "M";
                        cellButton.IsMin = true;
                        _buttons[row, col] = cellButton;
                        //_buttons[row, col].Name = "M";
                    }
                    else
                    {
                        int nearMines = CountNearMines(row, col);

                        //cellButton.Name = "";
                        _buttons[row, col] = cellButton;
                        //nearMines > 0 ? cellButton.IsNumber = true : ;
                    }

                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    gameField.Children.Add(cellButton);
                }
            }
        }*/

        /*private void OnCellButtonClick(int row, int col)
        {
            Console.WriteLine($"{row}, {col}");
            if (click)
            {
                Console.WriteLine($"{row}, {col}");
                if (_modifyButtons[row,col] == true)
                {
                    GenerateGameField();
                    GenerateUIField();
                    Console.WriteLine($"{row}, {col}");
                    OnCellButtonClick(row, col);
                    click = false;
                }
            }

            if (_modifyButtons[row, col])
            {
                MessageBox.Show("Вы попали на мину!");
            }
            else
            {
                int nearMines = CountNearMines(row, col);
                _buttons[row, col].Content = nearMines > 0 ? nearMines.ToString() : " ";
                _buttons[row, col].IsEnabled = false;
            }
        }*/

    }
}
