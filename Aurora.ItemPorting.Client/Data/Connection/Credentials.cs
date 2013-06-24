using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data
{
    /// <summary>
    /// Class to store credentials
    /// </summary>
    public class Credentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Credentials(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
