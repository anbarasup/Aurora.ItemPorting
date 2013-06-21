using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using Cognizant.ItemPorting.Client.Utilities;
using System.Net;
using System.IO;
using Aurora.ItemPorting.Client.Data.Entities;
using System.Xml.Linq;

namespace Aurora.ItemPorting.Client.Data
{
    public class WebService : DataService
    {
        #region Constructor
        public WebService():this(null)
        {
            
        }

        public WebService(Connection connection)
            : base(connection)
        {

        }
        #endregion

        #region Public Methods
        [Action("http://sitecore.net/visual/", "GetDatabases")]
        public virtual IEnumerable<Database> GetDatabases()
        {
            XDocument result = this.Execute(
            (sb) =>
            {
                sb.AppendFormat(@"<credentials>
                                    <Password>{0}</Password> 
                                    <UserName>{1}</UserName>
                                  </credentials>"
                                , this.Connection.Credentials.Password
                                , this.Connection.Credentials.UserName);
            });

            return InflateDatabaseResponse(result);
        }

        [Action("http://sitecore.net/visual/", "AddFromTemplate")]
        public virtual Guid AddFromTemplate(Guid parentItemId, Guid templateId, string itemName)
        {
            XDocument result = this.Execute(
            (sb) =>
            {
                sb.AppendFormat(@"<id>{2}</id>
                                  <templateID>{3}</templateID>
                                  <name>{4}</name>
                                  <databaseName>{5}</databaseName>
                                  <credentials>
                                    <Password>{0}</Password> 
                                    <UserName>{1}</UserName>
                                  </credentials>"
                                , this.Connection.Credentials.Password
                                , this.Connection.Credentials.UserName
                                , parentItemId.ToString()
                                , templateId.ToString()
                                , itemName
                                , this.Connection.Database);
            });

            return InflateAddFromTemplateResponse(result);
        }

        [Action("http://sitecore.net/visual/", "GetChildren")]
        public virtual IEnumerable<Item> GetChildern(Guid parentItemId)
        {
            XDocument result = this.Execute(
            (sb) =>
            {
                sb.AppendFormat(@"<id>{2}</id>
                                  <databaseName>{3}</databaseName>
                                  <credentials>
                                    <Password>{0}</Password> 
                                    <UserName>{1}</UserName>
                                  </credentials>"
                                , this.Connection.Credentials.Password
                                , this.Connection.Credentials.UserName
                                , parentItemId.ToString()
                                , this.Connection.Database);
            });

            return InflateGetChildernResponse(result);
        }

        [Action("http://sitecore.net/visual/", "GetItemFields")]
        public virtual IEnumerable<ItemField> GetItemFields(Item item, string language, int version, bool isAllFields)
        {
            XDocument result = this.Execute(
            (sb) =>
            {
                sb.AppendFormat(@"<id>{2}</id>
                                  <language>{3}</language>
                                  <version>{4}</version>
                                  <allFields>{5}</allFields>
                                  <databaseName>{6}</databaseName>
                                  <credentials>
                                      <Password>{0}</Password>
                                      <UserName>{1}</UserName>
                                  </credentials>"
                                , this.Connection.Credentials.Password
                                , this.Connection.Credentials.UserName
                                , item.Id.ToString()
                                , language
                                , version.ToString()
                                , isAllFields.ToString().ToLower()
                                , this.Connection.Database);
            });

            return InflateGetItemFieldsResponse(result);
        }

        [Action("http://sitecore.net/visual/", "GetTemplates")]
        public virtual IEnumerable<Template> GetTemplates()
        {
            XDocument result = this.Execute(
            (sb) =>
            {
                sb.AppendFormat(@"<databaseName>{2}</databaseName>
                                  <credentials>
                                      <Password>{0}</Password>
                                      <UserName>{1}</UserName>
                                  </credentials>"
                                , this.Connection.Credentials.Password
                                , this.Connection.Credentials.UserName
                                , this.Connection.Database);
            });

            return InflateGetTemplatesResponse(result);
        }
        #endregion

        #region Private Methods

        private IEnumerable<Template> InflateGetTemplatesResponse(XDocument result)
        {
            var templateList = from templateElement in result.Descendants("template")
                               select new Template()
                               {
                                   Id = new Guid(templateElement.Attribute("id").Value)
                                   ,
                                   Name = templateElement.Value
                                   ,
                                   IconPath = templateElement.Attribute("icon").Value
                               };
            return templateList;
        }

        private IEnumerable<ItemField> InflateGetItemFieldsResponse(XDocument result)
        {
            var fieldList = from fieldElement in result.Descendants("field")
                            select new ItemField()
                            {
                                ItemId = new Guid(fieldElement.Attribute("itemid").Value)
                                ,
                                Language = fieldElement.Attribute("language").Value
                                ,
                                Version = Convert.ToInt32(fieldElement.Attribute("version").Value)
                                ,
                                FieldID = new Guid(fieldElement.Attribute("fieldid").Value)
                                ,
                                Name = fieldElement.Attribute("name").Value
                                ,
                                Title = fieldElement.Attribute("title").Value
                                ,
                                Type = fieldElement.Attribute("type").Value
                                ,
                                Source = fieldElement.Attribute("source").Value
                                ,
                                Section = fieldElement.Attribute("section").Value
                                ,
                                Tooltip = fieldElement.Attribute("tooltip").Value
                                , 
                                Content = fieldElement.Value
                            };

            return fieldList;
        }

        private IEnumerable<Item> InflateGetChildernResponse(XDocument result)
        {
            var itemList = from item in result.Descendants("item")
                           select new Item()
                           {
                               Name = (string)item.Value
                               ,
                               Id = new Guid(item.Attribute("id").Value)
                               ,
                               IconPath = item.Attribute("icon").Value
                               ,
                               HasChildern = item.Attribute("haschildern") != null ? item.Attribute("haschildern").Value == "1" ? true : false : false
                           };

            return itemList;
        }

        private Guid InflateAddFromTemplateResponse(XDocument result)
        {
            Guid newItemID = Guid.Empty;
            var q = (from dataItem in
                         (from item in result.Descendants("data")
                          select item.Descendants("data"))
                     select dataItem).First();

            if (q != null)
            {
                foreach (XElement element in q)
                {
                    if (element != null)
                    {
                        if (!Guid.TryParse(element.Value, out newItemID))
                        {
                            newItemID = Guid.Empty;
                        }
                    }
                }
            }

            return newItemID;
        }

        private IEnumerable<Database> InflateDatabaseResponse(XDocument result)
        {
            var q = from database in result.Descendants("database")
                    select new Database(){Name = (string)database.Value};

            return q;
        } 
        #endregion
    }
}
