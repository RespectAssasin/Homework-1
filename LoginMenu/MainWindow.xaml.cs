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

namespace LoginMenu
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
        private List<string> _userNames = new List<string>();
        private List<string> _emails = new List<string>();
        private List<string> _passwords = new List<string>();

        public List<string> UserNames { get { return _userNames; } }
        public List<string> Emails { get { return _emails; } }
        public List<string> Passwords { get { return _passwords; } }

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
            if (UsernameBox.Text == string.Empty || PasswordBox.Password == string.Empty) 
            {
                MessageBox.Show("Ошибка, вы не заполнили все поля");
                return;
            }
            User user = new User(UsernameBox.Text, PasswordBox.Password);

            if (!user.IsExist(UserNames, Emails, Passwords))
            {
                MessageBox.Show($"Ошибка, пользователь не найден\nВозможно неправильный логин или пароль{user.UserName}, {user.Email}, {user.Password}, {user.IsExist(UserNames, Emails, Passwords)}");
                return;
            } else
            {
                LoginPanel.Visibility = Visibility.Collapsed;
                UserPanel.Visibility = Visibility.Visible;

                UsernameInfo.Text = user.UserName;
                EmailInfo.Text = user.Email;
                PasswordInfo.Text = user.Password;
            }
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            if (UserNameBox.Text == string.Empty || EmailBox.Text == String.Empty || FirstPasswordBox.Password == string.Empty || SecondPasswordBox.Password == string.Empty) 
            {
                MessageBox.Show("Ошибка, неправильный логин или пароль");
                return;
            }
            if (FirstPasswordBox.Password != SecondPasswordBox.Password)
            {
                MessageBox.Show("Ошибка, Первый пароли не одинаковы");
            }

            User user = new User(UserNameBox.Text, EmailBox.Text, FirstPasswordBox.Password);
            user.AddUser(_userNames, _emails, _passwords);
            if (!user.IsExist(UserNames, Emails, Passwords))
            {
                MessageBox.Show("Пользователь не добавлен\nps(не надо ломать меня)");
            } else
            {
                MessageBox.Show($"Поздравляем с регистрацией.\nПерезайдите в приложение, чтобы войти в аккаунт\n{user.IsExist(UserNames, Emails, Passwords)}, ");
                

                RegistrationPanel.Visibility = Visibility.Collapsed;
                MainMenuPanel.Visibility = Visibility.Visible;

                UserNameBox.Text = string.Empty;
                EmailBox.Text = string.Empty;
                FirstPasswordBox.Password = string.Empty;
                SecondPasswordBox.Password = string.Empty;
            }
            
        }

        
    }
}
