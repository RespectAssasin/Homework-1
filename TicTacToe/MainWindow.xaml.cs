using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TicTacToe
{
    public partial class MainWindow : Window
    {
        private TicTacToeLogic _logic;
        private string _gameType;
        private int _difficult;
        private char _userChar;

        public MainWindow()
        {
            InitializeComponent();
            this._gameType = "";
            this._difficult = 1;
            this._userChar = '\0';
        }

        private async void StartGame(object sender, RoutedEventArgs e)
        {
            if (this._gameType == "" || this._userChar == '\0')
            {
                MessageBox.Show("Вы не заполнили все поля");
                return;
            }
            if (this._gameType != "PvP" && this._difficult == 0)
            {
                MessageBox.Show("Вы не заполнили сложность");
                return;
            }
            this.PvESettingsPanel.Visibility = Visibility.Collapsed;
            this.GamePanel.Visibility = Visibility.Visible;

            if ((bool)RadioButt_X.IsChecked)
            {
                this._userChar = 'X';
            }
            else if ((bool)RadioButt_O.IsChecked)
            {
                this._userChar = 'O';
            }
            else
            {
                MessageBox.Show("Вы не выбрали ни одного символа");
                return;
            }

            this._logic = new TicTacToeLogic();
            this.TurnBox.Text = this._logic.CurrentPlayer;

            if (this._gameType == "PvE")
            {
                if (this._userChar == 'O' && this._logic.CurrentPlayer == "X")
                {
                    await Task.Delay(500);
                    this._logic.MakeAiMove(this._difficult, "X");
                    UpdateBoardUI();
                    if (this._logic.GameOver)
                    {
                        EndGame();
                    }
                    else
                    {
                        this.TurnBox.Text = this._logic.CurrentPlayer;
                    }
                }
            }
        }

        private Image GetSymbolImage(string symbol)
        {
            if (string.IsNullOrEmpty(symbol)) return null;
            string imageFile = symbol == "X" ? "X.png" : "O.png";
            Image img = new Image();
            Uri uri = new Uri("pack://application:,,,/" + imageFile, UriKind.Absolute);
            BitmapImage bmp = new BitmapImage(uri);
            img.Source = bmp;
            img.Stretch = System.Windows.Media.Stretch.Uniform;
            img.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            return img;
        }

        private void UpdateBoardUI()
        {
            Butt00.Content = GetSymbolImage(_logic.Board[0]);
            Butt01.Content = GetSymbolImage(_logic.Board[1]);
            Butt02.Content = GetSymbolImage(_logic.Board[2]);
            Butt10.Content = GetSymbolImage(_logic.Board[3]);
            Butt11.Content = GetSymbolImage(_logic.Board[4]);
            Butt12.Content = GetSymbolImage(_logic.Board[5]);
            Butt20.Content = GetSymbolImage(_logic.Board[6]);
            Butt21.Content = GetSymbolImage(_logic.Board[7]);
            Butt22.Content = GetSymbolImage(_logic.Board[8]);
        }

        private void EndGame()
        {
            Butt00.IsEnabled = false;
            Butt01.IsEnabled = false;
            Butt02.IsEnabled = false;
            Butt10.IsEnabled = false;
            Butt11.IsEnabled = false;
            Butt12.IsEnabled = false;
            Butt20.IsEnabled = false;
            Butt21.IsEnabled = false;
            Butt22.IsEnabled = false;

            PlayAgainButton.IsEnabled = true;

            if (_logic.Winner == "Draw")
            {
                MessageBox.Show("Ничья!");
            }
            else
            {
                MessageBox.Show("Победил " + _logic.Winner);
            }
        }
        private async void GameClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Name.Length >= 2)
            {
                int x, y;
                if (int.TryParse(button.Name[button.Name.Length - 2].ToString(), out x) &&
                    int.TryParse(button.Name[button.Name.Length - 1].ToString(), out y))
                {
                    if (this._logic != null && !this._logic.GameOver)
                    {
                        if (this._gameType == "PvE" && this._logic.CurrentPlayer != this._userChar.ToString())
                            return;

                        this._logic.MakeMove(x, y);
                        UpdateBoardUI();
                        this.TurnBox.Text = this._logic.CurrentPlayer;

                        if (this._logic.GameOver)
                        {
                            EndGame();
                            return;
                        }
                        if (this._gameType == "PvE")
                        {
                            await Task.Delay(500);
                            if (!this._logic.GameOver)
                            {
                                this._logic.MakeAiMove(this._difficult, this._logic.CurrentPlayer);
                                UpdateBoardUI();
                                if (this._logic.GameOver)
                                {
                                    EndGame();
                                }
                                else
                                {
                                    this.TurnBox.Text = this._logic.CurrentPlayer;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не удалось извлечь координаты из имени кнопки.");
                }
            }
        }

        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            if (this._logic != null)
            {
                this._logic.ResetGame();
                Butt00.Content = "";
                Butt01.Content = "";
                Butt02.Content = "";
                Butt10.Content = "";
                Butt11.Content = "";
                Butt12.Content = "";
                Butt20.Content = "";
                Butt21.Content = "";
                Butt22.Content = "";
                this.TurnBox.Text = "";
            }
            Butt00.IsEnabled = true;
            Butt01.IsEnabled = true;
            Butt02.IsEnabled = true;
            Butt10.IsEnabled = true;
            Butt11.IsEnabled = true;
            Butt12.IsEnabled = true;
            Butt20.IsEnabled = true;
            Butt21.IsEnabled = true;
            Butt22.IsEnabled = true;

            this.GamePanel.Visibility = Visibility.Collapsed;
            this.PvESettingsPanel.Visibility = Visibility.Collapsed;
            this.MainMenuPanel.Visibility = Visibility.Visible;

            this._gameType = "";
            this._difficult = 0;
            this._userChar = '\0';
        }

        private void GoToPvPSettings(object sender, RoutedEventArgs e)
        {
            this._gameType = "PvP";
            this._userChar = 'X';
            this.MainMenuPanel.Visibility = Visibility.Collapsed;
            this.PvESettingsPanel.Visibility = Visibility.Collapsed;
            this.GamePanel.Visibility = Visibility.Visible;

            this._logic = new TicTacToeLogic();
            this.TurnBox.Text = this._logic.CurrentPlayer;
            UpdateBoardUI();
        }

        private void GoToPvESettings(object sender, RoutedEventArgs e)
        {
            this._gameType = "PvE";
            this.MainMenuPanel.Visibility = Visibility.Collapsed;
            this.GamePanel.Visibility = Visibility.Collapsed;
            this.PvESettingsPanel.Visibility = Visibility.Visible;
        }

        private async void GoToEvESettings(object sender, RoutedEventArgs e)
        {
            this._gameType = "EvE";
            this.MainMenuPanel.Visibility = Visibility.Collapsed;
            this.PvESettingsPanel.Visibility = Visibility.Collapsed;
            this.GamePanel.Visibility = Visibility.Visible;

            this._logic = new TicTacToeLogic();
            this.TurnBox.Text = this._logic.CurrentPlayer;
            UpdateBoardUI();
            await StartEvEGame();
        }

        private async void GoToGamePanel(object sender, RoutedEventArgs e)
        {
            if (this._gameType == "PvE")
            {
                if (this.RadioButt_X.IsChecked == true)
                {
                    this._userChar = 'X';
                }
                else if (this.RadioButt_O.IsChecked == true)
                {
                    this._userChar = 'O';
                }
                else
                {
                    MessageBox.Show("Вы не выбрали, за кого вы ходите.");
                    return;
                }
            }
            PvESettingsPanel.Visibility = Visibility.Collapsed;
            MainMenuPanel.Visibility = Visibility.Collapsed;
            GamePanel.Visibility = Visibility.Visible;

            _logic = new TicTacToeLogic();
            TurnBox.Text = _logic.CurrentPlayer;

            PlayAgainButton.IsEnabled = false;

            if (_gameType == "PvE")
            {
                if (_userChar == 'O' && _logic.CurrentPlayer == "X")
                {
                    await Task.Delay(500);
                    _logic.MakeAiMove(_difficult, "X");
                    UpdateBoardUI();
                    if (_logic.GameOver)
                    {
                        EndGame();
                    }
                    else
                    {
                        TurnBox.Text = _logic.CurrentPlayer;
                    }
                }
            }
            if (_gameType == "EvE")
            {
                await StartEvEGame();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this._difficult = (int)e.NewValue;
            switch (this._difficult)
            {
                case 1:
                    this.DifficultSliderBlock.Text = "Лёгкая";
                    break;
                case 2:
                    this.DifficultSliderBlock.Text = "Нормальная";
                    break;
                case 3:
                    this.DifficultSliderBlock.Text = "Сложная";
                    break;
            }
        }
        private async void PlayAgain(object sender, RoutedEventArgs e)
        {
            if (this._logic != null)
            {
                this._logic.ResetGame();
                Butt00.Content = "";
                Butt01.Content = "";
                Butt02.Content = "";
                Butt10.Content = "";
                Butt11.Content = "";
                Butt12.Content = "";
                Butt20.Content = "";
                Butt21.Content = "";
                Butt22.Content = "";
                Butt00.IsEnabled = true;
                Butt01.IsEnabled = true;
                Butt02.IsEnabled = true;
                Butt10.IsEnabled = true;
                Butt11.IsEnabled = true;
                Butt12.IsEnabled = true;
                Butt20.IsEnabled = true;
                Butt21.IsEnabled = true;
                Butt22.IsEnabled = true;
                this.TurnBox.Text = this._logic.CurrentPlayer;
                UpdateBoardUI();
                
            }
            if (this._gameType == "EvE")
            {
                _ = StartEvEGame();
            }
            if (_gameType == "PvE" && _userChar == 'O')
            {
                await Task.Delay(500);
                this._logic.MakeAiMove(this._difficult, "X");
                UpdateBoardUI();
                if (this._logic.GameOver)
                {
                    EndGame();
                }
                else
                {
                    this.TurnBox.Text = this._logic.CurrentPlayer;
                }
            }
        }

        private async Task StartEvEGame()
        {
            if (this._logic == null) return;
            while (!this._logic.GameOver)
            {
                await Task.Delay(500);
                this._logic.MakeAiMove(3, this._logic.CurrentPlayer);
                UpdateBoardUI();
                if (this._logic.GameOver)
                {
                    EndGame();
                    break;
                }
                else
                {
                    this.TurnBox.Text = this._logic.CurrentPlayer;
                }
            }
        }
    }
}
