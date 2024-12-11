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
        //private bool [,] _gameField;
        private int _minesNumber;
        private bool _firstClick = true;

        private ModifyButton[,] _modifyButtons;

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            //CreateField();
            //GenerateGameField();
            //GenerateUIField();
        }
        private void Start(object sender, RoutedEventArgs e)
        {
            GamePanel.Visibility = Visibility.Visible;
            
            _modifyButtons = new ModifyButton[10, 10];
            for (int i = 0; i < _gameFieldHight; i++)
            {
                for (int j = 0; j < _gameFieldWidth; j++)
                {
                    _modifyButtons[i, j] = new ModifyButton
                    {
                        FontSize = 16,
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray
                    };
                }
            }

            CreateField();
        }

        private void CreateField()
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
                    Grid.SetRow(_modifyButtons[row, col], row);
                    Grid.SetColumn(_modifyButtons[row, col], col);
                    gameField.Children.Add(_modifyButtons[row, col]);
                }
            }
        }

        private void Button_Click()
        {
            if (_firstClick)
            {

            }
        }

        private void GenerateGameField()
        {
            //ClearArr(_gameField);
            //_gameField = new bool[_gameFieldWidth, _gameFieldHight];
            _minesNumber = (int)((float)(_gameFieldHight * _gameFieldWidth) * _minesPercent);

            int placedMines = 0;

            while (placedMines < _minesNumber)
            {
                int row = rand.Next(0, _gameFieldHight);
                int col = rand.Next(0, _gameFieldWidth);

                if (!_gameField[col, row] &&
                    (col == 0 || !_gameField[col - 1, row]) &&
                    (col == _gameFieldWidth - 1 || !_gameField[col + 1, row]) &&
                    (row == 0 || !_gameField[col, row - 1]) &&
                    (row == _gameFieldHight - 1 || !_gameField[col, row + 1]))
                {
                    _gameField[col,row] = true;
                    placedMines++;
                }
                else
                {
                    placedMines--;
                }
            }
        }

        /*private int CountNearMines(int row, int col)
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
                        !(i == 0 && j == 0)) 
                    {
                        if (_gameField[newRow, newCol])
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        private void GenerateUIField()
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

                    if (_gameField[row, col])
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
        }

        private void OnCellButtonClick(int row, int col)
        {
            Console.WriteLine($"{row}, {col}");
            if (_firstClick)
            {
                Console.WriteLine($"{row}, {col}");
                if (_gameField[row,col] == true)
                {
                    GenerateGameField();
                    GenerateUIField();
                    Console.WriteLine($"{row}, {col}");
                    OnCellButtonClick(row, col);
                    _firstClick = false;
                }
            }

            if (_gameField[row, col])
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
