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
        }
        
        private void GenerateGameField()
        {
            _gameField = new bool[_gameFieldWidth, _gameFieldHight];
            _minesNumber = (int)((float)(_gameFieldHight * _gameFieldWidth) * _minesPercent);
        
            for (int i = 0; i < _minesNumber; i++ )
            {
                int row = Next(_gameFieldWidth);
            }
        }

        private void GenerateUIField()
        {
            Grid gameField = new Grid();
            GamePanel.Children.Add(gameField);
            gameField.Margin = new Thickness(80, 130, 80, 10);

            for (int i = 0; i < _gameFieldHight; i++)
            {
                RowDefinition newRow = new RowDefinition();
                gameField.RowDefinitions.Add(newRow);
            }

        }

    }
}
