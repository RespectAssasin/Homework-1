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


        //private bool [,] _modifyButtons;
        private ModifyButton[,] _modifyButtons;

        private UserClick click = new UserClick();

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
            /*CreateField();
            //GenerateGameField();
            //GenerateUIField();*/
        }
        private void Start(object sender, RoutedEventArgs e)
        {
            GamePanel.Visibility = Visibility.Visible;
            
            _modifyButtons = new ModifyButton[10, 10];

            for (int row = 0; row < _gameFieldHight; row++)
            {
                for (int col = 0; col < _gameFieldWidth; col++)
                {
                    _modifyButtons[row, col] = new ModifyButton
                    {
                        FontSize = 16,
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray,
                        row = row,
                        col = col
                    };
                    _modifyButtons[row, col].Click += (s, args) => Button_Click(row, col);
                }
            }
            DrawField();
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

            while (countMines < _minesNumber)
            {
                int row = rand.Next(0, _gameFieldHight);
                int col = rand.Next(0, _gameFieldWidth);

                if (row >= 0 && row < _gameFieldHight &&
                    col >= 0 && col < _gameFieldWidth &&
                    !_modifyButtons[row, col].IsMine &&
                    (row == 0 || !_modifyButtons[row - 1, col].IsMine) &&
                    (row == _gameFieldHight - 1 || !_modifyButtons[row + 1, col].IsMine) &&
                    (col == 0 || !_modifyButtons[row, col - 1].IsMine) &&
                    (col == _gameFieldWidth - 1 || !_modifyButtons[row, col + 1].IsMine))
                {
                    _modifyButtons[row, col].IsMine = true;
                    countMines++;
                }
            }
        }

        private void Button_Click(int row, int col)
        {
            if (click.IsFirstClick)
            {
                click.row = row;
                click.col = col;

                if (click.row >= 0 && click.row < _gameFieldHight &&
                    click.col >= 0 && click.col < _gameFieldWidth)
                {
                    while (_modifyButtons[click.row, click.col].IsMine)
                    {
                        GenerateField();
                    }
                    click.IsFirstClick = false;
                    DrawField();
                }
                else
                {
                    MessageBox.Show("Некорректные координаты первого клика!");
                }
            }
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
                        if (_modifyButtons[newRow, newCol])
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
        }

        private void OnCellButtonClick(int row, int col)
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
