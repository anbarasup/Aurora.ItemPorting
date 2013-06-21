using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Core
{
    public class FactoryElement<T> : IFactoryElement where T : new()
    {
        public object New()
        {
            return new T();
        }
    }
}
