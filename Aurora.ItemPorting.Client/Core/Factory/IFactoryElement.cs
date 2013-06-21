using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aurora.ItemPorting.Client.Core
{
    public interface IFactoryElement
    {
        object New();
    }
}
