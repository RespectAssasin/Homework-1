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

        private char[][] arr = new char[3][];

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
                            arr[3][3] = (char)button.Content;
                            MessageBox.Show($"Значение '{arr[3][3]}' добавлено в массив на место ({x}, {y})");
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
    }
}
