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
using System.Globalization;
using System.Threading;

namespace LoginMenu
{
    /*UsernameBox.Text = string.Empty;
            EmailBox.Text = string.Empty;
            PasswordBox.Password = string.Empty;
            PhoneBox.Text = string.Empty;*/
    public partial class MainWindow : Window
    {
        private List<User> _users = new List<User>();
        
        private void UpdateUI()
        {
            WelcomeBlock.Text = Translation.WelcomeBlock;
            LoginButt.Content = Translation.LoginButt;
            RegisButt.Content = Translation.RegisButt;
            //ChangeLanguage.Content = Translation.ChangeLanguage;

            LoginBlock.Text = Translation.LoginBlock;
            EmailBlock.Text = Translation.EmailBlock;
            PasswordBlock.Text = Translation.PasswordBlock;
            LoginButton.Content = Translation.LoginButton;

            UsernameBlock.Text = Translation.UsernameBlock;
            EmailTextBlock.Text = Translation.EmailTextBlock;
            PasswordTextBlock.Text = Translation.PasswordTextBlock;
            PasswordBlockConfirm.Text = Translation.PasswordBlockConfirm;
            RegistrationBlock.Text = Translation.RegistrationBlock;
            RegistrationBlock.Text = Translation.RegistrationBlock;
            RegistrationButt.Content = Translation.RegistrationButt;
            DontHaveAcc.Text = Translation.DontHaveAcc;

            UserInfo.Text = Translation.UsernameInfo;
            UsernameInfoBlock.Text = Translation.UsernameInfoBlock;
            EmailInfoBlock.Text = Translation.EmailInfoBlock;
            PasswordInfoBlock.Text = Translation.PasswordInfoBlock;
            PhoneInfoBlock.Text = Translation.PhoneInfoBlock;

        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoToLogin(object sender, RoutedEventArgs e)
        {
            MainMenuPanel.Visibility = Visibility.Collapsed;
            RegistrationPanel.Visibility = Visibility.Collapsed;
            UserPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Visible;
        }

        private void GoToRegistration(object sender, RoutedEventArgs e)
        {
            MainMenuPanel.Visibility = Visibility.Collapsed;
            RegistrationPanel.Visibility = Visibility.Visible;
            UserPanel.Visibility = Visibility.Collapsed;
            LoginPanel.Visibility = Visibility.Collapsed;
        }

        private void GoToMenu(object sender, RoutedEventArgs e)
        {
            UserPanel.Visibility = Visibility.Collapsed;
            MainMenuPanel.Visibility = Visibility.Visible;
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Ошибка, вы не заполнили все поля");
                return;
            }

            var user = _users.FirstOrDefault(u =>
                (u.UserName == UsernameBox.Text || u.Email == UsernameBox.Text || u.PhoneNumber == UsernameBox.Text) &&
                u.Password == PasswordBox.Password);

            if (user == null)
            {
                MessageBox.Show("Ошибка, пользователь не найден. Проверьте данные для входа.");
                return;
            }

            LoginPanel.Visibility = Visibility.Collapsed;



            UserPanel.Visibility = Visibility.Visible;

            UsernameInfo.Text = user.UserName;
            EmailInfo.Text = user.Email;
            PasswordInfo.Text = user.Password;
            PhoneInfo.Text = user.PhoneNumber;
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserNameBox.Text) ||
                string.IsNullOrWhiteSpace(EmailBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneBox.Text) ||
                string.IsNullOrWhiteSpace(FirstPasswordBox.Password) ||
                string.IsNullOrWhiteSpace(SecondPasswordBox.Password))
            {
                MessageBox.Show("Ошибка, все поля обязательны для заполнения.");
                return;
            }

            if (FirstPasswordBox.Password != SecondPasswordBox.Password)
            {
                MessageBox.Show("Ошибка, пароли не совпадают.");
                return;
            }

            if (_users.Any(u => u.UserName == UserNameBox.Text || u.Email == EmailBox.Text || u.PhoneNumber == PhoneBox.Text))
            {
                MessageBox.Show("Ошибка, пользователь с такими данными уже существует.");
                return;
            }

            try
            {
                var newUser = new User(UserNameBox.Text, EmailBox.Text, PhoneBox.Text, FirstPasswordBox.Password);
                _users.Add(newUser);
                MessageBox.Show("Регистрация успешна. Теперь вы можете войти.");

                RegistrationPanel.Visibility = Visibility.Collapsed;
                MainMenuPanel.Visibility = Visibility.Visible;

                UserNameBox.Text = string.Empty;
                EmailBox.Text = string.Empty;
                PhoneBox.Text = string.Empty;
                FirstPasswordBox.Password = string.Empty;
                SecondPasswordBox.Password = string.Empty;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ChangeLanguageButtToEn(object sender, RoutedEventArgs e)
        {

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            UpdateUI();
        }


        private void ChangeLanguageToRU(object sender, RoutedEventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");

            UpdateUI();
        }
    }
}