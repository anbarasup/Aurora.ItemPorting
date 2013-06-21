using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using System.Diagnostics;
using Cognizant.ItemPorting.Client.Utilities;
using System.Xml.Linq;

namespace Aurora.ItemPorting.Client.Data
{
    public abstract class DataService : IDataService
    {
        public Connection Connection { get; set; }
        
        public DataService(Connection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Executes the SOAP requests
        /// </summary>
        /// <param name="actionEnvelopeFunc"></param>
        /// <returns></returns>
        public virtual XDocument Execute(Action<StringBuilder> actionEnvelopeFunc)
        {
            XDocument result = null;

            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();

            ActionAttribute actionAttr = Utility.GetAttribute<ActionAttribute>(method);

            if (actionAttr != null)
            {
                XmlDocument soapEnvelopeXml = CreateSoapEnvelope(actionEnvelopeFunc, actionAttr.ActionName, actionAttr.ActionUri);
                HttpWebRequest webRequest = CreateWebRequest(actionAttr.ActionUri, actionAttr.ActionName);
                InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request. 
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. 
                asyncResult.AsyncWaitHandle.WaitOne();
                
                // get the response from the completed web request. 
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (XmlReader reader = XmlReader.Create(webResponse.GetResponseStream()))
                    {
                        result = XDocument.Load(reader);
                    }
                }
            }

            return result;
        }

        protected virtual XmlDocument CreateSoapEnvelope(Action<StringBuilder> actionEnvelopeFunc, string actionName, Uri actionUri)
        {
            XmlDocument soapEnvelop = new XmlDocument();

            //building the SOAP envelope 
            StringBuilder soapEnvelopSB = new StringBuilder();

            soapEnvelopSB.Append(GetStartSoapEnvelopeElement());
            soapEnvelopSB.Append(GetStartSoapBodyElement());

            soapEnvelopSB.Append(GetStartActionElement(actionName, actionUri));
            actionEnvelopeFunc(soapEnvelopSB);
            soapEnvelopSB.Append(GetEndActionElement(actionName));

            soapEnvelopSB.Append(GetEndSoapBodyElement());
            soapEnvelopSB.Append(GetEndSoapEnvelopeElement());

            soapEnvelop.LoadXml(soapEnvelopSB.ToString());

            return soapEnvelop;
        }

        protected virtual HttpWebRequest CreateWebRequest(Uri actionUri, string actionName)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(this.Connection.ServiceUri);
            webRequest.Headers.Add("SOAPAction", actionUri.ToString() +  actionName);
            webRequest.ContentType = " text/xml;charset=\"utf-8\"";
            webRequest.Accept = " text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        protected void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            { 
                soapEnvelopeXml.Save(stream); 
            }
        }

        protected string GetStartSoapEnvelopeElement()
        {
            return @"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" 
                     xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" 
                     xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">";
        }

        protected string GetEndSoapEnvelopeElement()
        {
            return @"</soap:Envelope>";
        }


        protected string GetStartSoapBodyElement()
        {
            return @"<soap:Body>";
        }

        protected string GetEndSoapBodyElement()
        {
            return @"</soap:Body>";
        }

        protected string GetStartActionElement(string actionName, Uri actionUri)
        {
            return string.Format(@"<{0} xmlns=""{1}"">", actionName, actionUri.ToString());
        }

        protected string GetEndActionElement(string actionName)
        {
            return string.Format(@"</{0}>", actionName);
        } 
    }
}
