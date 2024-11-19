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
        
        private string _userName;
        private string _email;
        private string _password;
        private string _nameoremail;
        private int id1;
        private int id2;

        public string UserName { get { return _userName; } }
        public string Email { get { return _email; } }
        public string Password { get { return _password; } }
        public string NameOrEmail {  get { return _nameoremail; } }
        public void SetEmail(string email)
        {
            this._email = email;
        }
        public void SetUsername(string username)
        {
            this._userName = username;
        }
        public User() { }
        public User(string username, string email, string password) 
        {
            _userName = username;
            _email = email;
            _password = password;
        }
        public User(string nameOrEmail, string password)
        {
            _nameoremail = nameOrEmail;
            _password = password;
        }
        public bool IsExist(List<string> usernames, List<string> emails, List<string> passwords)
        {
            if (this.UserName != string.Empty && usernames.Contains(this.UserName))
            {
                id1 = usernames.IndexOf(this.UserName);
                id2 = passwords.IndexOf(this.Password);
                
                if (id1 == id2)
                {
                    this.SetEmail(emails[id1]);
                    return true;
                } 
            }
            else if (this.Email != string.Empty && emails.Contains(this.Email))
            {
                id1 = emails.IndexOf(this.Email);
                id2 = passwords.IndexOf(this.Password);

                if (id1 == id2)
                {
                    this.SetUsername(usernames[id1]);
                    return true;
                }
            }
            return false;
        }
        public void AddUser(List<string> usernames, List<string> emails, List<string>  passwords)
        {
            usernames.Add(this.UserName);
            emails.Add(this.Email);
            passwords.Add(this.Password);
        }
    }
}
