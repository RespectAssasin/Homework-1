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
        private bool [,] _gameField;
        private int _minesNumber;
        private bool _firstClick = true;
        public MainWindow()
        {
            InitializeComponent();
            GenerateGameField();
            GenerateUIField();
        }
        Random rand = new Random();
        private Button[,] _buttons;

        private void ClearArr(bool[,] arr)
        {
            for (int i = 0; i < _gameFieldWidth; i++)
            {
                for (int j = 0; j < _gameFieldHight; j++)
                {
                    arr[i, j] = false;
                }
            }
        }

        private void GenerateGameField()
        {
            //ClearArr(_gameField);
            _gameField = new bool[_gameFieldWidth, _gameFieldHight];
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
                /*else
                {
                    placedMines--;
                }*/
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
            _buttons = new Button[_gameFieldHight, _gameFieldWidth];
            Grid gameField = new Grid();
            GamePanel.Children.Clear();
            GamePanel.Children.Add(gameField);
            gameField.Margin = new Thickness(50, 50, 50, 50);

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
                    Button cellButton = new Button
                    {
                        FontSize = 16,
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray
                    };

                    cellButton.Click += (s, e) => OnCellButtonClick(row, col);

                    _buttons[row, col] = cellButton;

                    if (_gameField[row, col])
                    {
                        /*cellButton.Content = "M";*/
                        /*cellButton.Background = Brushes.Red;*/
                        /*cellButton.Name = "M";*/
                    }
                    else
                    {
                        //int nearMines = CountNearMines(row, col);

                        //cellButton.Name = nearMines > 0 ? nearMines.ToString() : "0";
                    }

                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    gameField.Children.Add(cellButton);
                }
            }
        }

        private void OnCellButtonClick(int row, int col)
        {

            if (_firstClick)
            {
                if (_gameField[row,col] == true)
                {
                    GenerateGameField();
                    GenerateUIField();
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
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
