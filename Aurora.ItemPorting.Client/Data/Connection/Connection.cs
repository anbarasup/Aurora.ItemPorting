using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data
{
    /// <summary>
    /// Class to store Connection information
    /// </summary>
    public class Connection
    {
        public Credentials Credentials { get; set; }
        public Uri ServiceUri { get; set; }
        public string Database { get; set; }

        public Connection(Uri serviceUri, Credentials credentials, string database)
        {
            this.ServiceUri = serviceUri;
            this.Credentials = credentials;
            this.Database = database;
        }
    }
}
