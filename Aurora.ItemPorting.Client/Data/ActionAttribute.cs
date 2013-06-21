using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Data
{
    public class ActionAttribute : Attribute
    {
        private readonly string _actionName;
        private readonly Uri _actionUri;

        public string ActionName { get { return _actionName ?? string.Empty; } }
        public Uri ActionUri { get { return _actionUri; } }

        public ActionAttribute(string actionURL, string actionName)
        {
            this._actionName = actionName;
            this._actionUri = new Uri(actionURL);
        }
    }
}
