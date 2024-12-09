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
        public MainWindow()
        {
            InitializeComponent();
            GenerateGameField();
            GenerateUIField();
        }
        Random rand = new Random();
        
        
        private void GenerateGameField()
        {
            _gameField = new bool[_gameFieldWidth, _gameFieldHight];
            _minesNumber = (int)((float)(_gameFieldHight * _gameFieldWidth) * _minesPercent);

            int placedMines = 0;

            while (placedMines < _minesNumber)
            {
                int row = rand.Next(0, _gameFieldHight);
                int col = rand.Next(0, _gameFieldWidth);

                if (!_gameField[col,row] && (col == 0 || !_gameField[col-1,row]) && (col == _gameFieldWidth - 1 || !_gameField[col+1,row]))
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

        private void GenerateUIField()
        {
            Grid gameField = new Grid();
            GamePanel.Children.Clear();
            GamePanel.Children.Add(gameField);
            gameField.Margin = new Thickness(80, 130, 80, 10);

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
                        Background = Brushes.LightGray,
                        Content = _gameField[row, col] ? "M" : "O"
                    };

                    Grid.SetRow(cellButton, row);
                    Grid.SetColumn(cellButton, col);
                    gameField.Children.Add(cellButton);
                }
            }
        }

    }
}
