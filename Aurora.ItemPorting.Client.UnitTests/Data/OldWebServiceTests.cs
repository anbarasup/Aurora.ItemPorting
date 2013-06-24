using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurora.ItemPorting.Client.Data;
using Aurora.ItemPorting.Client.Data.Entities;

namespace Aurora.ItemPorting.Client.UnitTests.Data
{
    [TestClass]
    public class OldWebServiceTests
    {
        [TestMethod]
        public void GetDatabases()
        {
            WebService ows = DataServiceFactory.Instance.Create("OldWebSevice"
                                                                , new Connection(new Uri("http://Sitecore680/sitecore/shell/webservice/service.asmx")
                                                                , new Credentials(@"sitecore\admin", "b"), "master"));
            IEnumerable<Database> owsdata = ows.GetDatabases();
        }
    }
}
