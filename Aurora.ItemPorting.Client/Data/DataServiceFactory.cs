using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Core;

namespace Aurora.ItemPorting.Client.Data
{
    public class DataServiceFactory
    {
        Factory<string, WebService> dataserviceFactory = new Factory<string, WebService>();

        DataServiceFactory()
        {
            dataserviceFactory.Add<OldWebService>("OldWebSevice");
            dataserviceFactory.Add<HardRockService>("HardRockSevice");
        }

        private static readonly Lazy<DataServiceFactory> instance = new Lazy<DataServiceFactory>(() => new DataServiceFactory());
        public static DataServiceFactory Instance { get { return instance.Value; } }

        public WebService Create(string serviceType, Connection connection)
        {
            WebService dataService = dataserviceFactory.Create(serviceType);
            dataService.Connection = connection;
            return dataService;
        }
    }
}
