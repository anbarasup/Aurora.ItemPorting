using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Core;

namespace Aurora.ItemPorting.Client.Data
{
    [FactoryType("HardRockService", typeof(WebService))]
    public class HardRockService : WebService
    {
        public HardRockService():base()
        {

        }

        public HardRockService(Connection connection)
            :base(connection)
        {

        }
    }
}
