using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
    public class MailCredentionals
    {
        public MailCredentionals(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public string Login { get; }

        public string Password { get; }
    }
}
