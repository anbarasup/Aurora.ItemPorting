using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Data;
using Aurora.ItemPorting.Client.Data.Entities;

namespace Aurora.ItemPorting.Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            //ows.AddFromTemplate(new Guid("{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}"), new Guid("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}"), "TestSample3");
            //IEnumerable<Item> childern = ows.GetChildern(new Guid("{A0CCF70A-5F87-454B-8A1B-05EEA2DF5FD5}"));
            //IEnumerable<ItemField> itemfields = ows.GetItemFields(new Item() { Id = new Guid("{2B9A2A9B-8E69-43AB-BB6F-60AEC55CB364}") }, "en", 0, false);
            
            //OldWebService localsitecore = new OldWebService(new Connection(new Uri("http://SitecoreLocal:8080/sitecore/shell/webservice/service.asmx"), new Credentials(@"sitecore\admin", "b"), "master"));
            //IEnumerable<Database> databases = localsitecore.GetDatabases();
            //IEnumerable<Template> templates = localsitecore.GetTemplates();
            //foreach (Template template in templates)
            //{
            //    if (template.Sections != null)
            //        Console.WriteLine(template.Name);
            //}

            //OldWebService onsitesitecore = new OldWebService(new Connection(new Uri("http://cms.dev-int.Aurora.com/sitecore/shell/webservice/service.asmx"), new Credentials(@"sitecore\admin", "b"), "master"));
            //IEnumerable<Database> onsitedatabases = onsitesitecore.GetDatabases();
            //IEnumerable<Template> onsitetemplates = onsitesitecore.GetTemplates();
            //foreach (Template template in templates)
            //{
            //    if (template.Sections != null)
            //        Console.WriteLine(template.Name);
            //}

            WebService ows = DataServiceFactory.Instance.Create("OldWebSevice", new Connection(new Uri("http://SitecoreLocal:8080/sitecore/shell/webservice/service.asmx"), new Credentials(@"sitecore\admin", "b"), "master"));
            IEnumerable<Database> owsdata = ows.GetDatabases();
        }
    }
}
