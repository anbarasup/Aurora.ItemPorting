using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aurora.ItemPorting.Client.Data;

namespace Aurora.ItemPorting.Client.Comparer
{
    public class DBComparer
    {
        private WebService _source = null;
        private WebService _target = null;

        public DBComparer(WebService source, WebService target)
        {
            _source = source;
            _target = target;
        }
    }
}
