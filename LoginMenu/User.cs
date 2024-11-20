using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginMenu
{
    internal class User
    {
        public string UserName;
        public string Email;
        public string PhoneNumber;
        public string Password;

        public User(string userName, string email, string phoneNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phoneNumber) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Все поля обязательны для заполнения.");

            if (!IsValidEmail(email))
                throw new ArgumentException("Некорректный формат email. Email должен содержать '@' и заканчиваться на yandex.ru, mail.ru, gmail.com или inbox.ru.");
            if (Email.Substring(0, Email.IndexOf("@")).Length < 8)
                throw new ArgumentException("Некрректная заптсь почты.\nПочта должна быть длтной более 8 символов");

            if (!IsValidPassword(password))
                throw new ArgumentException("Пароль должен быть длиной не менее 8 символов, содержать хотя бы одну заглавную букву, одну строчную букву, один специальный символ и одну цифру.");

            if (userName.Length < 3 || userName.Length > 30)
                throw new ArgumentException("Имя пользователя должно быть длиной от 3 до 30 символов.");

            if (userName == password)
                throw new ArgumentException("Имя пользователя не должно совпадать с паролем.");

            var emailPrefix = email.Split('@')[0];
            if (userName == emailPrefix)
                throw new ArgumentException("Имя пользователя не должно совпадать с первой частью email.");

            if (!IsValidPhoneNumber(phoneNumber))
                throw new ArgumentException("Некорректный формат номера телефона. Должен содержать только цифры и начинаться с '+'.");
            if (phoneNumber.Length != 12)
                throw new ArgumentException("Некорректная записб номера телефона.\nНапишите свой настоящий номер телефона");

            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        private bool IsValidEmail(string email)
        {
            if (!email.Contains("@"))
                return false;

            string[] validDomains = { "yandex.ru", "mail.ru", "gmail.com", "inbox.ru" };
            string domain = email.Split('@').Last();
            return validDomains.Contains(domain);
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpper && hasLower && hasDigit && hasSpecial;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.StartsWith("+") && phoneNumber.Skip(1).All(char.IsDigit) ;
        }
    }
}