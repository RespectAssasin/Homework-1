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

namespace TicTacToe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        double Difficulty = 0;

        TicTacToeLogic logic;

        private string _gameType = "";
        private int _difficult = 0;
        private char _userChar = '\0';


        //private char[][] arr = new char[3][];

        private void GameClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                string buttonName = button.Name;
                if (buttonName.Length >= 2)
                {
                    int x, y;
                    if (int.TryParse(buttonName[buttonName.Length - 2].ToString(), out x) &&
                        int.TryParse(buttonName[buttonName.Length - 1].ToString(), out y))
                    {
                        if (x >= 0 && x < 3 && y >= 0 && y < 3)
                        {
                            //arr[3][3] = (char)button.Content;

                            //MessageBox.Show($"Значение '{arr[3][3]}' добавлено в массив на место ({x}, {y})");
                        }
                        else
                        {
                            MessageBox.Show("Координаты за пределами допустимого диапазона.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось извлечь координаты из имени кнопки.");
                    }
                }
                else
                {
                    MessageBox.Show("Имя кнопки слишком короткое.");
                }
            }

        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            GamePanel.Visibility = Visibility.Collapsed;
            PvPSettingsPanel.Visibility = Visibility.Collapsed;
            PvESettingsPanel.Visibility = Visibility.Collapsed;
            MainMenuPanel.Visibility = Visibility.Visible;
            _gameType = "";
            _difficult = 0;
            _userChar = ' ';
        }

        private void GoToPvPSettings(object sender, RoutedEventArgs e)
        {
            MainMenuPanel.Visibility = Visibility.Collapsed;
            PvPSettingsPanel.Visibility = Visibility.Visible;
            _gameType = "PvP";
        }
        private void GoToPvESettings(object sender, RoutedEventArgs e)
        {
            MainMenuPanel.Visibility = Visibility.Collapsed;
            PvESettingsPanel.Visibility = Visibility.Visible;
            _gameType = "PvE";
        }
        private void GoToEvESettings(object sender, RoutedEventArgs e)
        {
            MainMenuPanel.Visibility = Visibility.Collapsed;
            PvESettingsPanel.Visibility = Visibility.Visible;
            _gameType = "EvE";
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            Difficulty = DifficultySlider.Value;

            /*if (DifficultySlider.Value == 1 && DifficultySlider.Value > 1)
            {
                DifficultySlider.Value = 2;
            } else if (DifficultySlider.Value == 2 && DifficultySlider.Value > 2)
            {
                DifficultySlider.Value = 3;
            } else if (DifficultySlider.Value == 3 && DifficultySlider.Value < 3)
            {
                DifficultySlider.Value = 2;
            } else if (DifficultySlider.Value == 2 && DifficultySlider.Value < 2)
            {
                DifficultySlider.Value = 1;
            }*/

            switch ((int)Difficulty)
            {
                case 1:
                    DifficultSliderBlock.Text = "Лёгкая";
                    break;
                case 2:
                    DifficultSliderBlock.Text = "Нормальная";
                    break;
                case 3:
                    DifficultSliderBlock.Text = "Сложная";
                    break;
            }
        }

        private void StartGame(object sender, RoutedEventArgs e)
        {

            if (_gameType == "" || _userChar == '\0')
            {
                MessageBox.Show("Вы не заполнили все поля");
                return;
            }
            if (!(_gameType == "PvP") && _difficult == 0)
            {
                MessageBox.Show("Вы не заполнили сложность");
                return;
            }

            PvPSettingsPanel.Visibility = Visibility.Collapsed;
            PvESettingsPanel.Visibility = Visibility.Collapsed;
            GamePanel.Visibility = Visibility.Visible;

            if ((bool)RadioButt_X.IsChecked)
            {
                _userChar = 'X';
            }
            else if ((bool)RadioButt_O.IsChecked)
            {
                _userChar = 'O';
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одного символа");
                return;
            }

            logic = new TicTacToeLogic();

        }
    }
}
