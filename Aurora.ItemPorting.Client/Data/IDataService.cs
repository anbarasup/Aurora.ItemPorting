using System;
using System.Xml.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data
{
    public interface IDataService
    {
        Connection Connection { get; set; }
        XDocument Execute(Action<StringBuilder> actionEnvelopeFunc);
    }
}
